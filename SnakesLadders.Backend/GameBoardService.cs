using SnakesLadders.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakesLadders.Backend
{
    public class GameBoardService
    {
        public GameBoard InitializeGameBoard(int gameSpaceTotal, int maxSpacesPerTurn = 6)
        {
            var board = new GameBoard(gameSpaceTotal);

            for(int i = 1; i <= gameSpaceTotal; ++i)
            {
                if(i + maxSpacesPerTurn > gameSpaceTotal)
                {
                    // handling the last spaces of the board
                    for(int j = gameSpaceTotal; j > i; --j)
                    {
                        board.AdjacencyList[i].AddFirst(board.Spaces[j]);
                    }
                }
                else
                {
                    for(int k = 1; k <= maxSpacesPerTurn; ++k)
                    {
                        board.AdjacencyList[i].AddLast(board.Spaces[i + k]);
                    }
                }
            }
            
            return board;
        }

        public void ApplyGameSpaceSubstitutions(GameBoard board, List<GameSpaceSubstitution> substitutions, int maxSpacesPerTurn = 6)
        {
            if (board == null || substitutions == null) return;

            foreach(var substitution in substitutions)
            {
                if (substitution.Start.HasSubstitution || substitution.End.HasSubstitution) continue;

                for (int i = 1; i <= maxSpacesPerTurn; ++i)
                {
                    if ((substitution.Start.Id - i) < 1) continue;
                    var previousGameSpaceAdjacentList = board.AdjacencyList[substitution.Start.Id - i];
                    var substitutionStartNode = previousGameSpaceAdjacentList.Find(substitution.Start);
                    if(substitutionStartNode != null) substitutionStartNode.Value = substitution.End;
                }

                substitution.Start.Substitution.End = substitution.End;
                substitution.End.Substitution.Start = substitution.Start;
                board.AdjacencyList[substitution.Start.Id].Clear();
            }
        }

        public List<GameSpaceSubstitution> GenerateGameSpaceSubstitutions(GameBoard board, List<(int,int)> gameSpaceIndicies)
        {
            if (board == null || gameSpaceIndicies == null) return new List<GameSpaceSubstitution>();

            var substitutions = new List<GameSpaceSubstitution>(gameSpaceIndicies.Count);
            foreach(var indicies in gameSpaceIndicies)
            {
                if (indicies.Item1 > 0 && indicies.Item2 > 0 && indicies.Item1 < board.Spaces.Count && indicies.Item2 < board.Spaces.Count)
                {
                    substitutions.Add(new GameSpaceSubstitution { Start = board.Spaces[indicies.Item1], End = board.Spaces[indicies.Item2] });
                }
            }
            return substitutions;
        }

        public void BreadthFirstSearch(GameBoard board, int startId = 1)
        {
            StringBuilder sb = new StringBuilder();
            string endl = System.Environment.NewLine;
            int level, nodeId;
            bool printedLevel;
            LinkedList<GameSpace> queue = new LinkedList<GameSpace>();
            LinkedListNode<GameSpace> currentNode;
            LinkedList<GameSpace> currentNodeAdjList;
            board.GameSpaceLevels[startId] = 0;
            board.ParentOfGameSpace[startId] = new GameSpace { Id = int.MinValue };
            queue.AddFirst(board.Spaces[startId]);

            while(queue.Count > 0)
            {
                currentNode = queue.Last;
                queue.RemoveLast();
                Console.WriteLine($"Current node is {currentNode.Value.Id:D4};\tLevel:{board.GameSpaceLevels[currentNode.Value.Id]:D2};\tParent:{board.ParentOfGameSpace[currentNode.Value.Id].Id:D4}");
                currentNodeAdjList = board.AdjacencyList[currentNode.Value.Id];
                printedLevel = false;
                sb.Clear();

                for(var adjacentNode = currentNodeAdjList.First; adjacentNode != null; adjacentNode = adjacentNode.Next)
                {
                    level = board.GameSpaceLevels[currentNode.Value.Id] + 1;
                    nodeId = adjacentNode.Value.Id;

                    if (!printedLevel)
                    {
                        sb.Append($"\tLevel:{level:D2}{endl}");
                        printedLevel = true;
                    }

                    if (!board.GameSpaceLevels.ContainsKey(nodeId))
                    {
                        sb.Append($"\t\tAdjacent Node:{nodeId:D4};\tParent:{currentNode.Value.Id:D4}{endl}");
                        board.GameSpaceLevels[nodeId] = level;
                        board.ParentOfGameSpace[nodeId] = currentNode.Value;
                        queue.AddFirst(adjacentNode.Value);
                    }
                }

                Console.WriteLine(sb.ToString());
            }
        }

    }
}

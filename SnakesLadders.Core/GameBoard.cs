
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakesLadders.Core
{
    public class GameBoard
    {
        public List<GameSpace> Spaces { get; set; }
        public List<LinkedList<GameSpace>> AdjacencyList { get; set; }
        public Dictionary</*Id of GameSpace*/int, /*Parent of GameSpace*/GameSpace> ParentOfGameSpace { get; set; }
        public Dictionary</*Id of GameSpace*/int, /*Level of GameSpace*/int> GameSpaceLevels { get; set; }

        public GameBoard() : this(-1)
        {
        }

        public GameBoard(int gameSpaceTotal)
        {
            Spaces = new List<GameSpace>(gameSpaceTotal + 1);
            AdjacencyList = new List<LinkedList<GameSpace>>(gameSpaceTotal + 1);
            ParentOfGameSpace = new Dictionary<int, GameSpace>();
            GameSpaceLevels = new Dictionary<int, int>();

            if (gameSpaceTotal < 1) return;
            for (int i = 0; i <= gameSpaceTotal; ++i)
            {
                Spaces.Add(new GameSpace { Id = i });
                AdjacencyList.Add(new LinkedList<GameSpace>());
            }
        }

        public void Reset()
        {
            ParentOfGameSpace.Clear();
            GameSpaceLevels.Clear();
        }

        public void PrintAdjacencyList()
        {
            StringBuilder sb = new StringBuilder();
            LinkedList<GameSpace> curentGameSpaceList;
            GameSpace currentGameSpace;

            for(int i = 0; i < AdjacencyList.Count; ++i)
            {
                sb.Append($"Adj. List Node {i:D4}:");
                curentGameSpaceList = AdjacencyList[i];
                currentGameSpace = Spaces[i];
                
                if(curentGameSpaceList.Count < 1 && currentGameSpace.HasSubstitution)
                {
                    if(currentGameSpace.Substitution.Type == GameSpaceSubstitutionTypes.Beginning)
                    {
                        curentGameSpaceList = AdjacencyList[currentGameSpace.Substitution.End.Id];
                        sb.Append($"\t*[{currentGameSpace.Substitution.End.Id:D4}]");
                    }
                }

                foreach (var gameSpace in curentGameSpaceList)
                {
                    sb.Append($"\t-> {gameSpace.Id:D4}");
                }
                sb.Append(System.Environment.NewLine);
            }

            Console.WriteLine(sb.ToString());
        }

        public void PrintNodeHistory(int nodeIndex = 0)
        {
            StringBuilder sb = new StringBuilder();
            LinkedList<GameSpace> list = new LinkedList<GameSpace>();
            int id, offset;
            GameSpace tempSpace;
            var currentSpace = (nodeIndex > 0 && nodeIndex < Spaces.Count) ? Spaces[nodeIndex] : Spaces.LastOrDefault();

            // Ensure a "substituted" node was selected. If sure select then next previous node that has an adjacency list.
            id = currentSpace.Id;
            if (!ParentOfGameSpace.TryGetValue(id, out tempSpace))
            {
                for(int i = 1; i <= 6; ++i) // Reverse up-to 6 spaces - one die roll
                {
                    if ((id - i) > 0 && ParentOfGameSpace.TryGetValue(id - i, out tempSpace))
                    {
                        list.AddFirst(currentSpace);
                        currentSpace = Spaces[id - i];
                        offset = i;
                        break;
                    }
                }
            }

            // build list of nodes using each node's parent
            while (currentSpace != null && currentSpace.Id != int.MinValue)
            {
                list.AddFirst(currentSpace);
                id = currentSpace.Id;
                ParentOfGameSpace.TryGetValue(id, out currentSpace);
            }

            for(var currentNode = list.First; currentNode != null; currentNode = currentNode.Next)
            {
                if (sb.Length > 0) sb.Append(" -> ");
                sb.Append($"[{currentNode.Value.Id:D4}]");
            }
            sb.Insert(0, "Best path found: ");
            Console.WriteLine(sb.ToString());
        }
    }
}

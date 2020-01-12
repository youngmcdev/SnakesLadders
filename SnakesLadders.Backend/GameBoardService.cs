using SnakesLadders.Core;
using System.Collections.Generic;

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
                        board.AdjacencyList[i].AddLast(board.Spaces[j]);
                    }
                }
                else
                {
                    for(int k = 1; k <= maxSpacesPerTurn; ++k)
                    {
                        board.AdjacencyList[i].AddFirst(board.Spaces[i + k]);
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
                for (int i = 1; i <= maxSpacesPerTurn; ++i)
                {
                    if ((substitution.Start.Id - i) < 1) continue;
                    var adjacentSpaces = board.AdjacencyList[substitution.Start.Id - i];
                    var space = adjacentSpaces.Find(substitution.Start);
                    space.Value = substitution.End;
                }

                board.AdjacencyList[substitution.Start.Id].Clear();
            }
        }

        public List<GameSpaceSubstitution> GenerateGameSpaceSubstitutions(GameBoard board, List<(int,int)> gameSpaceIndicies)
        {
            if (board == null || gameSpaceIndicies == null) return new List<GameSpaceSubstitution>();

            var substitutions = new List<GameSpaceSubstitution>(gameSpaceIndicies.Count);
            foreach(var indicies in gameSpaceIndicies)
            {
                substitutions.Add(new GameSpaceSubstitution { Start = board.Spaces[indicies.Item1], End = board.Spaces[indicies.Item2] });
            }
            return substitutions;
        }
    }
}

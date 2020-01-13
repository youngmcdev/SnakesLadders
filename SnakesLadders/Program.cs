using SnakesLadders.Backend;
using System.Collections.Generic;

namespace SnakesLadders
{
    class Program
    {
        private static readonly int _gameSize = 100;
        
        static void Main(string[] args)
        {
            var svc = new GameBoardService();
            var gameBoard = svc.InitializeGameBoard(_gameSize);
            var substitutions = svc.GenerateGameSpaceSubstitutions(gameBoard, new List<(int, int)> 
            { 
                 //(3,54), (37,100), (56,33) // Test case: Should result in Best path found: [0001] -> [0054] -> [0033] -> [0100]
                 (4,14), (9,31), (20,38), (28,84), (40,59), (51,67), (63,81), (71,91), // Ladders
                 (17,7), (54,34), (62,19), (64,60), (87,24), (93,73), (95,75), (99,78) // Snakes
            });
            svc.ApplyGameSpaceSubstitutions(gameBoard, substitutions);
            gameBoard.PrintAdjacencyList();
            svc.BreadthFirstSearch(gameBoard, 1);
            gameBoard.PrintNodeHistory();
            int x = 0;
        }
    }
}

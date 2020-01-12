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
                (4,14) 
            });
            svc.ApplyGameSpaceSubstitutions(gameBoard, substitutions);
            int x = 0;
        }
    }
}


using System.Collections.Generic;

namespace SnakesLadders.Core
{
    public class GameBoard
    {
        public List<GameSpace> Spaces { get; set; }
        public List<LinkedList<GameSpace>> AdjacencyList { get; set; }
        public Dictionary</*Id of GameSpace*/int, /*Parent of GameSpace*/GameSpace> ParentOfGameSpace { get; set; }
        public Dictionary</*Id of GameSpace*/int, /*Level of GameSpace*/int> GameSpaceLevels { get; set; }

        public GameBoard()
        {
            Spaces = new List<GameSpace>();
            AdjacencyList = new List<LinkedList<GameSpace>>();
        }

        public GameBoard(int gameSpaceTotal)
        {
            Spaces = new List<GameSpace>(gameSpaceTotal + 1);
            AdjacencyList = new List<LinkedList<GameSpace>>(gameSpaceTotal + 1);
            ParentOfGameSpace = new Dictionary<int, GameSpace>();
            GameSpaceLevels = new Dictionary<int, int>();

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
    }
}

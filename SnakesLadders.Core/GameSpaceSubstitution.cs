
namespace SnakesLadders.Core
{
    public class GameSpaceSubstitution
    {
        public GameSpace Start { get; set; }
        public GameSpace End { get; set; }
        public GameSpaceSubstitutionTypes Type
        {
            get
            {
                if (Start != null && End != null) return GameSpaceSubstitutionTypes.Both;
                if (Start != null && End == null) return GameSpaceSubstitutionTypes.End;
                if (Start == null && End != null) return GameSpaceSubstitutionTypes.Beginning;
                return GameSpaceSubstitutionTypes.None;
            }
        }
    }

    public enum GameSpaceSubstitutionTypes
    {
        None = 0,
        Beginning,
        End,
        Both
    }
}

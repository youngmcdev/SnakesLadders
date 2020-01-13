
namespace SnakesLadders.Core
{
    public class GameSpace
    {
        public int Id { get; set; }
        public GameSpaceSubstitution Substitution { get; set; }
        public bool HasSubstitution
        {
            get
            {
                return Substitution != null && (Substitution.Start != null || Substitution.End != null);
            }
        }

        public GameSpace()
        {
            Substitution = new GameSpaceSubstitution();
        }
    }
}

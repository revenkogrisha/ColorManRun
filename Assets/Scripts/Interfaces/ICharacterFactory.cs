using SaveTheGuy.Characters;

namespace SaveTheGuy.Factories
{
    public interface ICharacterFactory
    {
        public Character InitStats(Character character, StatsConfig config);
    }
}

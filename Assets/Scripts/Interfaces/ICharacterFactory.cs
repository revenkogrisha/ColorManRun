using ColorManRun.Characters;

namespace ColorManRun.Factories
{
    public interface ICharacterFactory
    {
        public Character InitStats(Character character, StatsConfig config);
    }
}

using ColorManRun.Characters;

namespace ColorManRun.Factories
{
    public class CharacterFactory : ICharacterFactory
    {
        public Character InitStats(Character character, StatsConfig config)
        {
            character.InitStats(config);
            return character;
        }
    }
}

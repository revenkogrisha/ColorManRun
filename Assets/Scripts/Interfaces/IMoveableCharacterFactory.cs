using ColorManRun.Characters;

namespace ColorManRun.Factories
{
    public interface IMoveableCharacterFactory : ICharacterFactory
    {
        public T SetupMovement<T>(T character) where T : Character;
    }
}

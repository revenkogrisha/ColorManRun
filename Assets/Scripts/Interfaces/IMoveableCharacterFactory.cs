using SaveTheGuy.Characters;

namespace SaveTheGuy.Factories
{
    public interface IMoveableCharacterFactory : ICharacterFactory
    {
        public T SetupMovement<T>(T character) where T : Character;
    }
}

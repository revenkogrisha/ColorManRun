using ColorManRun.Characters;

namespace ColorManRun.Factories
{
    public class AgentCharacterFactory : CharacterFactory, IMoveableCharacterFactory
    {
        private readonly AgentTarget _target;

        public AgentCharacterFactory(AgentTarget target)
        {
            _target = target;
        }

        public T SetupMovement<T>(T character)
            where T : Character
        {
            if (!character.TryGetComponent<AgentMovement>(out var agentMovement))
                return character;

            agentMovement.SetTarget(_target);
            agentMovement.ApplyTargetLayer();
            agentMovement.ApplySpeed();

            return character;
        }
    }
}

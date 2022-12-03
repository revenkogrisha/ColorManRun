public class AgentCharacterFactory : IMoveableCharacterFactory
{
    private AgentTarget _target;

    public AgentCharacterFactory(AgentTarget target)
    {
        _target = target;
    }

    public Character InitStats(Character character, StatsConfig config)
    {
        character.InitStats(config);
        return character;
    }

    public Character SetupMovement(Character character)
    {
        if (!character.TryGetComponent<AgentMovement>(out var agentMovement))
            return character;

        agentMovement.SetTarget(_target);
        agentMovement.ApplyTargetLayer();
        agentMovement.ApplySpeed();

        return character;
    }
}

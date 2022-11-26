using UnityEngine;

public class CharacterFactory : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private StatsConfig _statsConfig;

    public Character InitCharacter(Character character)
    {
        character = InitStats(character, _statsConfig);
        character = TrySetupMovement(character, _target);
        return character;
    }

    private Character InitStats(Character character, StatsConfig config)
    {
        character.InitStats(config);
        return character;
    }

    private Character TrySetupMovement(Character character, Transform target)
    {
        var agentMovement = character.GetComponent<AgentMovement>();
        if (!agentMovement)
        {
            Debug.Log("AgentMovement lost");
            return character;
        }

        agentMovement.SetTarget(target);
        agentMovement.ApplyTargetLayer();
        agentMovement.ApplySpeed();
        return character;
    }
}

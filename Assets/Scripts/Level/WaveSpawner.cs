using UnityEngine;
using UnityTools;

[RequireComponent(typeof(Timer))]
public class WaveSpawner : MonoBehaviour
{
    [Header("Ground Check")]
    [Tooltip("Choose what should be defined as ground. Characters won't spawn on other layers")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Characters")]
    [SerializeField] private Character _characterPrefab;
    [Tooltip("Set if factory uses NavMesh or demands it")]
    [SerializeField] private AgentTarget _target;
    [SerializeField] private StatsConfig _statsConfig;

    [Header("Spawn settings")]
    [Tooltip("Transform that characters would be spawned around")]
    [SerializeField] private Transform _originTransform;
    [SerializeField] [Range(0f, 20f)] private float _spawnRadius = 15f;
    [SerializeField] [Range(0, 4)] private int _countInGroup = 3;

    private ICharacterFactory _factory;
    private GroundCheck _groundCheck;
    private Timer _timer;
    private readonly float _checkGroundRadius = 1.2f;

    #region MonoBehaviour

    private void Awake() => InitFields();

    private void OnEnable() => _timer.OnCooldownPassed += SpawnRandomWave;

    private void OnDisable() => _timer.OnCooldownPassed -= SpawnRandomWave;

    #endregion

    public void SpawnRandomWave()
    {
        var i = 0;
        while (i < _countInGroup)
        {
            var spawnPosition = GetPositionInCircle(_originTransform.position, _spawnRadius);
            var isOnGround = _groundCheck.CheckGroundOnPosition(spawnPosition, _checkGroundRadius);
            if (!isOnGround)
                continue;

            SpawnUnit(spawnPosition);

            i++;
        }
    }

    private void InitFields()
    {
        _factory = new AgentCharacterFactory(_target);
        _groundCheck = new(_groundLayer);
        _timer = GetComponent<Timer>();
    }

    private void SpawnUnit(Vector3 spawnPosition)
    {
        var character = Instantiate(_characterPrefab, spawnPosition, Quaternion.identity);

        character = _factory.InitStats(character, _statsConfig);
        character = TrySetupMovement(character, _factory);
    }

    private Vector3 GetPositionInCircle(Vector3 center, float radius)
    {
        var offset = Random.insideUnitCircle.normalized;
        offset *= radius;

        return new Vector3(
            center.x + offset.x,
            center.y,
            center.z + offset.y);
    }

    private Character TrySetupMovement(Character character, ICharacterFactory factory)
    {
        if (factory is not IMoveableCharacterFactory moveableFactory)
            return character;

        character = moveableFactory.SetupMovement(character);
        return character;
    }
}

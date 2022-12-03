using UnityEngine;
using UnityTools;

[RequireComponent(typeof(Timer))]
public class WaveSpawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GroundCheck _groundCheck;

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
    private Timer _timer;

    #region MonoBehaviour

    private void Awake()
    {
        _factory = new AgentCharacterFactory(_target);
        _timer = GetComponent<Timer>();
    }

    private void OnEnable()
    {
        _timer.OnCooldownPassed += SpawnRandomWave;
    }

    private void OnDisable()
    {
        _timer.OnCooldownPassed -= SpawnRandomWave;
    }

    #endregion

    public void SpawnRandomWave()
    {
        var i = 0;
        while (i < _countInGroup)
        {
            var spawnPosition = GetPositionInCircle(_originTransform.position, _spawnRadius);
            if (!_groundCheck.CheckGroundOnPosition(spawnPosition))
                continue;

            var character = Instantiate(_characterPrefab, spawnPosition, Quaternion.identity);

            character = _factory.InitStats(character, _statsConfig);
            character = TrySetupMovement(character, _factory);

            i++;
        }
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

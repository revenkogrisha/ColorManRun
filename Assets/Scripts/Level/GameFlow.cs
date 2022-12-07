using UnityEngine;
using UnityEngine.AI;

public class GameFlow : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlatformGenerator _platformGenerator;
    [SerializeField] private CharacterSpawner _characterSpawner;

    [Header("Player")]
    [SerializeField] private Joystick _joystick;
    [SerializeField] private OrbitCamera _orbitCamera;

    [Header("Prefabs")]
    [SerializeField] private PlayerCharacter _playerPrefab;
    [SerializeField] private TargetCharacter _targetPrefab;

    [Header("Level")]
    [SerializeField] private FinishTarget _finish;
    [SerializeField] private NavMeshSurface _navSurface;
    [SerializeField] private Vector3 _playerSpawnPosition = Vector3.forward;
    [SerializeField] private Vector3 _targetSpawnPosition = Vector3.zero;
        
    private LevelGenerator _levelGenerator;
    private PlayerJoystickInput _playerInput;

    #region MonoBehaviour

    private void Awake()
    {
        _levelGenerator = new(_platformGenerator,
            _characterSpawner, 
            _playerPrefab, 
            _targetPrefab);
    }

    private void OnEnable()
    {
        _levelGenerator.OnPlayerSpawned += TrySetupPlayerInput;
        _levelGenerator.OnPlayerSpawned += SetupCamera;
    }
    
    private void OnDisable()
    {
        _levelGenerator.OnPlayerSpawned -= TrySetupPlayerInput;
        _levelGenerator.OnPlayerSpawned -= SetupCamera;
    }

    private void Start() =>
        _levelGenerator.GenerateLevel(
            _finish,
            _navSurface,
            _playerSpawnPosition,
            _targetSpawnPosition);

    private void Update() => TryMovePlayer();

    #endregion

    private void TrySetupPlayerInput(PlayerCharacter player)
    {
        if (!player.TryGetComponent<RelativeMovement>(out var movement))
            throw new System.Exception("No RelativeMovement.cs has been found on Player!");

        _playerInput = new(_joystick, movement);
    }

    private void SetupCamera(PlayerCharacter player)
    {
        var playerTransform = player.transform;
        _orbitCamera.SetTarget(playerTransform);

        var playerPosition = playerTransform.position;
        _orbitCamera.Init(playerPosition);
    }

    private void TryMovePlayer() => _playerInput.Move();
}

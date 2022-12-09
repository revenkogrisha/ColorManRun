using SaveTheGuy.Characters;
using SaveTheGuy.Control;
using UnityEngine;
using UnityEngine.AI;

namespace SaveTheGuy.Level
{
    public class GameFlow : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private PlatformGenerator _platformGenerator;
        [SerializeField] private CharacterSpawner _characterSpawner;

        [Header("Player")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Camera _camera;

        [Header("Prefabs")]
        [SerializeField] private PlayerCharacter _playerPrefab;
        [SerializeField] private TargetCharacter _targetPrefab;

        [Header("Level")]
        [SerializeField] private NavMeshSurface _navSurface;
        [SerializeField] private Vector3 _playerSpawnPosition = Vector3.forward;
        [SerializeField] private Vector3 _targetSpawnPosition = Vector3.zero;
        
        private OrbitCamera _orbitCamera;
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
            _levelGenerator.OnPlayerSpawned += InitCamera;
        }
    
        private void OnDisable()
        {
            _levelGenerator.OnPlayerSpawned -= TrySetupPlayerInput;
            _levelGenerator.OnPlayerSpawned -= InitCamera;
        }

        private void Start() => GenerateLevel(_levelGenerator);

        private void Update() => TryMovePlayer();

        private void LateUpdate() => _orbitCamera.TryApplyCameraTransform();

        #endregion

        private void TrySetupPlayerInput(PlayerCharacter player)
        {
            if (!player.TryGetComponent<RelativeMovement>(out var movement))
                throw new System.Exception("No RelativeMovement.cs has been found on Player!");

            _playerInput = new(_joystick, movement);
        }

        private void InitCamera(PlayerCharacter player)
        {
            var playerTransform = player.transform;
            var playerPosition = playerTransform.position;
            _orbitCamera = new(_camera, playerTransform, playerPosition);
        }

        private void GenerateLevel(LevelGenerator generator)
        {
            generator.GenerateLevel(
                _navSurface,
                _playerSpawnPosition,
                _targetSpawnPosition);
        }

        private void TryMovePlayer() => _playerInput.Move();
    }
}
using ColorManRun.Characters;
using ColorManRun.Control;
using UnityEngine;
using UnityEngine.AI;

namespace ColorManRun.Generators
{
    public class GameStart : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private PlatformGenerator _platformGenerator;
        [SerializeField] private CharacterSpawner _characterSpawner;

        [Header("Player")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private OrbitCamera _orbitCamera;

        [Header("Prefabs")]
        [SerializeField] private PlayerCharacter _playerPrefab;

        [Header("Level")]
        [SerializeField] private NavMeshSurface _navSurface;
        [SerializeField] private Vector3 _playerSpawnPosition = Vector3.forward;
        
        private LevelGenerator _levelGenerator;

        #region MonoBehaviour

        private void Awake()
        {
            _levelGenerator = new(
                _platformGenerator,
                _characterSpawner, 
                _playerPrefab);
        }

        private void OnEnable()
        {
            _levelGenerator.OnPlayerSpawned += InitCamera;
            _levelGenerator.OnPlayerSpawned += InitPlayerMovement;
        }
    
        private void OnDisable()
        {
            _levelGenerator.OnPlayerSpawned -= InitCamera;
            _levelGenerator.OnPlayerSpawned -= InitPlayerMovement;
        }

        private void Start()
        {
            GenerateLevel(_levelGenerator);
        }

        #endregion

        private void InitCamera(PlayerCharacter player)
        {
            var playerTransform = player.transform;
            var playerPosition = playerTransform.position;
            _orbitCamera.Init(playerTransform, playerPosition);
        }

        private void InitPlayerMovement(PlayerCharacter player)
        {
            if (!player.TryGetComponent<PlayerMovement>(out var movement))
                throw new System.Exception("No PlayerMovement was found on Player");

            movement.Init(_joystick);
        }

        private void GenerateLevel(LevelGenerator generator)
        {
            generator.GenerateLevel(
                _navSurface,
                _playerSpawnPosition);
        }
    }
}
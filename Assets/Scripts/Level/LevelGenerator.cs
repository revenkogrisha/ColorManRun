using SaveTheGuy.Characters;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace SaveTheGuy.Level
{
    public class LevelGenerator
    {
        private ISurfaceGenerator _surfaceGenerator;
        private INavMeshSurfaceGenerator _navGenerator;
        private CharacterSpawner _characterSpawner;
        private PlayerCharacter _playerPrefab;
        private TargetCharacter _targetPrefab;

        public event Action<TargetCharacter> OnTargetSpawned;
        public event Action<PlayerCharacter> OnPlayerSpawned;

        public LevelGenerator(
            ISurfaceGenerator surfaceGenerator, 
            CharacterSpawner characterSpawner, 
            PlayerCharacter playerPrefab, 
            TargetCharacter targetPrefab)
        {
            _surfaceGenerator = surfaceGenerator;
            _navGenerator = new NavMeshSurfaceGenerator();
            _characterSpawner = characterSpawner;
            _playerPrefab = playerPrefab;
            _targetPrefab = targetPrefab;
        }

        public void GenerateLevel(
            NavMeshSurface navSurface,
            Vector3 playerSpawnPosition, 
            Vector3 targetSpawnPosition)
        {
            var finish = GenerateSuface(navSurface);

            SpawnPlayer(playerSpawnPosition);
            SpawnTarget(targetSpawnPosition, finish);
        }

        private FinishTarget GenerateSuface(NavMeshSurface surface)
        {
            var finish = _surfaceGenerator.GenerateSurface();
            _navGenerator.GenerateSurface(surface);
            return finish;
        }

        private void SpawnPlayer(Vector3 playerSpawnPosition)
        {
            var player = _characterSpawner.Spawn(_playerPrefab, playerSpawnPosition);
            OnPlayerSpawned?.Invoke(player);
        }

        private void SpawnTarget(Vector3 targetSpawnPosition, FinishTarget finish)
        {
            var target = _characterSpawner.SpawnAgent(_targetPrefab, finish, targetSpawnPosition);
            OnTargetSpawned?.Invoke(target);
        }
    }
}

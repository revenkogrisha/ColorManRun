using ColorManRun.Characters;
using ColorManRun.ColorFeatures;
using ColorManRun.Level;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace ColorManRun.Generators
{
    public class LevelGenerator
    {
        private ISurfaceGenerator _surfaceGenerator;
        private INavMeshSurfaceGenerator _navGenerator;
        private CharacterSpawner _characterSpawner;
        private PlayerCharacter _playerPrefab;

        public event Action<PlayerCharacter> OnPlayerSpawned;

        public LevelGenerator(
            ISurfaceGenerator surfaceGenerator, 
            CharacterSpawner characterSpawner, 
            PlayerCharacter playerPrefab)
        {
            _surfaceGenerator = surfaceGenerator;
            _navGenerator = new NavMeshSurfaceGenerator();
            _characterSpawner = characterSpawner;
            _playerPrefab = playerPrefab;
        }

        public void GenerateLevel(
            NavMeshSurface navSurface,
            Vector3 playerSpawnPosition)
        {
            GenerateSuface(navSurface);

            SpawnPlayer(playerSpawnPosition);
        }

        private FinishTarget GenerateSuface(NavMeshSurface surface)
        {
            var colorPicker = new ColorTrioPicker();
            var colors = colorPicker.ColorTrio;
            var finish = _surfaceGenerator.GenerateSurface(colors);
            _navGenerator.GenerateSurface(surface);
            return finish;
        }

        private void SpawnPlayer(Vector3 playerSpawnPosition)
        {
            var player = _characterSpawner.Spawn(_playerPrefab, playerSpawnPosition);
            OnPlayerSpawned?.Invoke(player);
        }
    }
}

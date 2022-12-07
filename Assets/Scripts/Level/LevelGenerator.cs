using System;
using UnityEngine;
using UnityEngine.AI;

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
        FinishTarget finish,
        NavMeshSurface navSurface,
        Vector3 playerSpawnPosition, 
        Vector3 targetSpawnPosition)
    {
        GenerateSuface(navSurface);

        var player = _characterSpawner.Spawn(_playerPrefab, playerSpawnPosition);
        OnPlayerSpawned?.Invoke(player);

        var target = _characterSpawner.SpawnAgent(_targetPrefab, finish, targetSpawnPosition);
        OnTargetSpawned?.Invoke(target);
    }

    private void GenerateSuface(NavMeshSurface surface)
    {
        _surfaceGenerator.GenerateSurface();
        _navGenerator.GenerateSurface(surface);
    }
}

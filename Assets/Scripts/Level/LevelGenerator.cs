using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Platform[] _platformsPrefabs;
    [SerializeField] private int _platformsPerLevel = 3;

    private float _platformLength = 30f;
    private float _spawnOffset = 0f;

    private void Start()
    {
        GenerateRandom();
    }

    private void GenerateRandom()
    {
        for (var i = 0; i < _platformsPerLevel; i++)
            SpawnRandomPlatform();
    }

    private void SpawnRandomPlatform()
    {
        var random = Random.Range(0, _platformsPrefabs.Length);
        var platform = _platformsPrefabs[random];

        Instantiate(platform, Vector3.forward * _spawnOffset, Quaternion.identity);

        _spawnOffset += _platformLength;
    }
}

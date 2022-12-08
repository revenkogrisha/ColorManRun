using UnityEngine;

public class PlatformGenerator : MonoBehaviour, ISurfaceGenerator
{
    [Header("Platforms")]
    [SerializeField] private FinishPlatform _finishPlatformPrefab;
    [SerializeField] private Platform[] _platformsPrefabs;

    [Header("Settings")]
    [SerializeField] private int _platformsPerLevel = 3;

    private FinishTarget _finish;
    private readonly float _platformLength = 30f;
    private float _offset = 0f;

    public FinishTarget Finish
    {
        get
        {
            if (!_finish)
                throw new System.Exception("Finish is null!");

            return _finish;
        }
    }

    public FinishTarget GenerateSurface()
    {
        for (var i = 0; i < _platformsPerLevel; i++)
        {
            if (i == _platformsPerLevel - 1)
            {
                var platform = SpawnPlatform(_finishPlatformPrefab);
                _finish = GetFinishTarget(platform);
            }

            SpawnRandomPlatform();
        }

        return Finish;
    }

    private void SpawnRandomPlatform()
    {
        var random = Random.Range(0, _platformsPrefabs.Length);
        var platform = _platformsPrefabs[random];

        SpawnPlatform(platform);
        _offset += _platformLength;
    }

    private void SpawnPlatform(Platform platform)
    {
        var position = Vector3.forward * _offset;
        Instantiate(platform, position, Quaternion.identity);
    }

    private FinishPlatform SpawnPlatform(FinishPlatform prefab)
    {
        var position = Vector3.forward * _offset;
        var platform = Instantiate(prefab, position, Quaternion.identity);
        return platform;
    }

    private FinishTarget GetFinishTarget(FinishPlatform platform) => platform.Finish;
}

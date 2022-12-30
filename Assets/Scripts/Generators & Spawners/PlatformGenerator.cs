using ColorManRun.Level;
using UnityEngine;

namespace ColorManRun.Generators
{
    public class PlatformGenerator : MonoBehaviour, ISurfaceGenerator
    {
        [Header("Platforms")]
        [SerializeField] private FirstPlatform _firstPlatformPrefab;
        [SerializeField] private FinishPlatform _finishPlatformPrefab;
        [SerializeField] private ColorPlatform[] _platformsPrefabs;

        [Header("Platforms' parent")]
        [Tooltip("Opional")]
        [SerializeField] private LevelObjectsParent _parent;

        [Header("Settings")]
        [SerializeField] private int _platformsPerLevel = 3;

        private readonly float _platformLength = 30f;
        private float _offset = 0f;

        public FinishTarget GenerateSurface()
        {
            for (var i = 0; i < _platformsPerLevel; i++)
            {
                var lastPlatformIndex = _platformsPerLevel - 1;
                if (i == lastPlatformIndex)
                {
                    var finishTarget = SpawnFinishPlatform();
                    return finishTarget;
                }

                SpawnRandomPlatform();
            }

            throw new System.Exception("Finish wasn't assingned within 'for' cycle!");
        }

        private void SpawnRandomPlatform()
        {
            var random = Random.Range(0, _platformsPrefabs.Length);
            var platform = _platformsPrefabs[random];

            SpawnPlatform(platform);
            _offset += _platformLength;
        }

        private FinishTarget SpawnFinishPlatform()
        {
            var platform = SpawnPlatform(_finishPlatformPrefab);
            return GetFinishTarget(platform);
        }

        private T SpawnPlatform<T>(T prefab)
            where T : Platform
        {
            var position = Vector3.forward * _offset;
            var platform = Instantiate(prefab, position, Quaternion.identity);
            return TrySetParent(platform);
        }

        private T TrySetParent<T>(T platform)
            where T : Platform
        {
            if (_parent == null)
                return platform;

            var platformTransform = platform.transform;
            platformTransform.SetParent(_parent.transform);
            return platform;
        }

        private FinishTarget GetFinishTarget(FinishPlatform platform) => platform.Finish;
    }
}

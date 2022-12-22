using ColorManRun.Level;
using UnityEngine;

namespace ColorManRun.Generators
{
    public class PlatformGenerator : MonoBehaviour, ISurfaceGenerator
    {
        [Header("Platforms")]
        [SerializeField] private FinishPlatform _finishPlatformPrefab;
        [SerializeField] private SimplePlatform[] _platformsPrefabs;

        [Header("Platforms' parent")]
        [Tooltip("Opional")]
        [SerializeField] private LevelObjectsParent _parent;

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

        private T SpawnPlatform<T>(T prefab)
            where T : Platform
        {
            var position = Vector3.forward * _offset;
            T platform;
            if (_parent)
            {
                platform = Instantiate(prefab, position, Quaternion.identity);
                platform.transform.SetParent(_parent.transform);
            }
            else
            {
                platform = Instantiate(prefab, position, Quaternion.identity);
            }

            return platform;
        }

        private FinishTarget GetFinishTarget(FinishPlatform platform) => platform.Finish;
    }
}

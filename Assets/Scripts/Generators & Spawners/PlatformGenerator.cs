using ColorManRun.Level;
using ColorManRun.ColorFeatures;
using UnityEngine;

namespace ColorManRun.Generators
{
    public class PlatformGenerator : MonoBehaviour, ISurfaceGenerator
    {
        [Header("Components")]
        [SerializeField] private ColorTrioPicker _colorPicker;

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
            SpawnPlatform(_firstPlatformPrefab);
            for (var i = 1; i < _platformsPerLevel - 1; i++)
                SpawnRandomPlatform();

            return SpawnFinishPlatform();
        }

        private void SpawnRandomPlatform()
        {
            var randomPlatformIndex = Random.Range(0, _platformsPrefabs.Length);
            var platform = _platformsPrefabs[randomPlatformIndex];

            platform = SetPlatformColor(platform);

            SpawnPlatform(platform);
        }

        private FinishTarget SpawnFinishPlatform()
        {
            var platform = SpawnPlatform(_finishPlatformPrefab);
            return GetFinishTarget(platform);
        }

        private ColorPlatform SetPlatformColor(ColorPlatform platform)
        {
            var color = _colorPicker.GetRandomColor();
            var material = _colorPicker.GetMaterialByColor(color);

            platform.SetColor(color, material);
            return platform;
        }

        private T SpawnPlatform<T>(T prefab)
            where T : Platform
        {
            var position = Vector3.forward * _offset;
            var platform = Instantiate(prefab, position, Quaternion.identity);
            _offset += _platformLength;
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

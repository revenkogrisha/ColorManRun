using ColorManRun.ColorFeatures;
using UnityEngine;

namespace ColorManRun.Level
{
    public class ColorPlatform : Platform
    {
        [SerializeField] private MeshRenderer _groundRenderer;

        private GameColor _color;

        public GameColor Color => _color;

        public void SetColor(GameColor color, Material material)
        {
            _color = color;
            _groundRenderer.material = material;
        }
    }
}

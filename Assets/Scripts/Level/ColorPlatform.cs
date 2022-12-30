using UnityEngine;

namespace ColorManRun.Level
{
    public class ColorPlatform : Platform
    {
        private Color _color;

        public Color Color => _color;

        public void SetColor(Color color)
        {
            _color = color;
        }
    }
}

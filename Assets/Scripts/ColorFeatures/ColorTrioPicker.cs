using UnityEngine;

namespace ColorManRun.ColorFeatures
{
    public class ColorTrioPicker
    {
        private readonly Color[][] _colors = new Color[][]
        {
            new Color[] { Color.yellow, Color.blue, Color.green },
            new Color[] { Color.yellow, Color.blue, Color.green },
            new Color[] { Color.yellow, Color.blue, Color.green }
        };

        private Color[] _colorTrio;

        public Color[] ColorTrio
        {
            get
            {
                if (_colorTrio == null)
                    throw new System.Exception("Color trio wasn't assigned! Null");

                return _colorTrio;
            }
        }

        public ColorTrioPicker()
        {
            DetermineColorTrio();
        }

        private void DetermineColorTrio()
        {
            var random = Random.Range(0, _colors.Length);
            var trio = _colors[random];
            _colorTrio = trio;
        }
    }
}

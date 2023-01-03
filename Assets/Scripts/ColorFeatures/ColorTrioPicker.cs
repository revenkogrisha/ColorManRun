using System.Collections.Generic;
using UnityEngine;

namespace ColorManRun.ColorFeatures
{
    public class ColorTrioPicker : MonoBehaviour
    {
        [Header("Materials")]
        [SerializeField] private Material _blue;
        [SerializeField] private Material _red;
        [SerializeField] private Material _green;

        private Dictionary<GameColor, Material> _router;
        private GameColor[] _colors;

        #region MonoBehaviour

        private void Awake()
        {
            InitColors();
            InitDictionary();
        }

        #endregion

        public GameColor GetRandomColor()
        {
            var randomColorsIndex = Random.Range(0, _colors.Length);
            return _colors[randomColorsIndex];
        }

        public Material GetMaterialByColor(GameColor color)
        {
            if (!_router.ContainsKey(color))
                throw new System.Exception("No related material to requested color in dictionary!");

            return _router[color];
        }

        private void InitColors()
        {
            _colors = new GameColor[]
            {
                GameColor.Blue,
                GameColor.Green,
                GameColor.Red
            };
        }

        private void InitDictionary()
        {
            _router = new();
            _router[GameColor.Blue] = _blue;
            _router[GameColor.Green] = _green;
            _router[GameColor.Red] = _red;
        }
    }
}

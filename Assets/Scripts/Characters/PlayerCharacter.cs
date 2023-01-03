using ColorManRun.ColorFeatures;
using UnityEngine;

namespace ColorManRun.Characters
{
    public class PlayerCharacter : CharacterAwakeInit
    {
        [SerializeField] private Renderer _renderer;

        private GameColor _color;

        private void SetColor(GameColor color, Material material)
        {
            _color = color;
            _renderer.material = material;
        }
    }
}

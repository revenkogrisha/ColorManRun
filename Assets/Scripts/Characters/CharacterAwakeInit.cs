using UnityEngine;

namespace SaveTheGuy.Characters
{
    public class CharacterAwakeInit : Character
    {
        [SerializeField] private StatsConfig _statsConfig;

        private void Awake() => InitStats(_statsConfig);
    }
}

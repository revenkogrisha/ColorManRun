using UnityEngine;

namespace SaveTheGuy.Level
{
    public class FinishPlatform : Platform
    {
        [SerializeField] private FinishTarget _finish;

        public FinishTarget Finish => _finish;
    }
}

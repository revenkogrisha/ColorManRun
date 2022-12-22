using UnityEngine;

namespace ColorManRun.Level
{
    public class FinishPlatform : Platform
    {
        [SerializeField] private FinishTarget _finish;

        public FinishTarget Finish => _finish;
    }
}

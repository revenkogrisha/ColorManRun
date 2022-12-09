using UnityEngine;

namespace SaveTheGuy.Level
{
    public class FinishPlatform : MonoBehaviour
    {
        [SerializeField] private FinishTarget _finish;

        public FinishTarget Finish => _finish;
    }
}

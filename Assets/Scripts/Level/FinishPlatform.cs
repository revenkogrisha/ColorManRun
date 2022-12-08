using UnityEngine;

public class FinishPlatform : MonoBehaviour
{
    [SerializeField] private FinishTarget _finish;

    public FinishTarget Finish => _finish;
}

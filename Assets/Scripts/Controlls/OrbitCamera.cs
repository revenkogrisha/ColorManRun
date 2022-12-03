using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Transform _transform;
    private Vector3 _offset;

    #region MonoBehaviour

    private void Awake()
    {
        _transform = transform;
        _offset = _target.position - _transform.position;
    }

    private void LateUpdate() => ApplyRotation();

    #endregion

    private void ApplyRotation()
    {
        _transform.position = _target.position - _offset;
        _transform.LookAt(_target);
    }
}

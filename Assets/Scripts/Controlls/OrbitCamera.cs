using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _cameraOffset = new(0f, 4f, -16f);

    private Transform _transform;
    private Vector3 _offset;

    #region MonoBehaviour

    private void LateUpdate() => TryApplyRotation();

    #endregion

    public void SetTarget(Transform target) => _target = target;

    public void Init(Vector3 playerPosition)
    {
        _transform = transform;
        if (!_target)
            throw new System.Exception("Target field is unassigned!");

        SetPosition(playerPosition);
        _offset = _target.position - _transform.position;
    }

    private void SetPosition(Vector3 playerPosition)
    {
        var cameraPosition = new Vector3(
            playerPosition.x + _cameraOffset.x,
            playerPosition.y + _cameraOffset.y,
            playerPosition.z + _cameraOffset.z);
        
        _transform.position = cameraPosition;
    }

    private void TryApplyRotation()
    {
        if (!_target)
            return;

        _transform.position = _target.position - _offset;
        _transform.LookAt(_target);
    }
}

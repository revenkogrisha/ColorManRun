using UnityEngine;

public class OrbitCamera
{
    private Transform _target;
    private Transform _transform;
    private Vector3 _cameraOffset = new(0f, 4f, -10f);
    private Vector3 _distanceToTarget;

    public OrbitCamera(Camera camera, Transform target, Vector3 playerPosition)
    {
        _transform = camera.transform;
        _target = target;

        SetPosition(playerPosition);
        _distanceToTarget = _target.position - _transform.position;
    }

    public void TryApplyCameraTransform()
    {
        if (!_target)
            return;

        _transform.position = _target.position - _distanceToTarget;
        _transform.LookAt(_target);
    }

    private void SetPosition(Vector3 playerPosition)
    {
        var cameraPosition = new Vector3(
            playerPosition.x + _cameraOffset.x,
            playerPosition.y + _cameraOffset.y,
            playerPosition.z + _cameraOffset.z);
        
        _transform.position = cameraPosition;
    }
}

using UnityEngine;

namespace ColorManRun.Control
{
    public class OrbitCamera : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Camera _camera;
        
        [Header("Settings")]
        [SerializeField] private Vector3 _cameraOffset = new(0f, 4f, -10f);

        private Transform _target;
        private Transform _transform;
        private Vector3 _distanceToTarget;

        public void Init(Transform target, Vector3 targetPosition)
        {
            _target = target;
            _transform = _camera.transform;

            SetPosition(targetPosition);
            _distanceToTarget = _target.position - _transform.position;
        }

        private void LateUpdate() => TryApplyCameraTransform();

        public void TryApplyCameraTransform()
        {
            if (!_target)
                return;

            _transform.position = _target.position - _distanceToTarget;
            _transform.LookAt(_target);
        }

        private void SetPosition(Vector3 targetPosition)
        {
            var cameraPosition = new Vector3(
                targetPosition.x + _cameraOffset.x,
                targetPosition.y + _cameraOffset.y,
                targetPosition.z + _cameraOffset.z);
        
            _transform.position = cameraPosition;
        }
    }
}

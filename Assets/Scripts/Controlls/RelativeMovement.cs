using SaveTheGuy.Characters;
using System;
using UnityEngine;

namespace SaveTheGuy.Control
{
    public class RelativeMovement : MonoBehaviour, IMoveable
    {
        [Header("Components")]
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;

        [Header("Settings")]
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _rotationSpeed = 10f;

        private Transform _transform;
        private MovementAnimator _characterAnimator;

        public event Action OnRunStarted;
        public event Action OnRunEnded;

        #region MonoBehaviour

        private void Awake()
        {
            _characterAnimator = new(_animator);
            _transform = transform;
        }

        private void OnEnable()
        {
            OnRunStarted += _characterAnimator.EnableRunning;
            OnRunEnded += _characterAnimator.DisableRunning;
        }
    
        private void OnDisable()
        {
            OnRunStarted -= _characterAnimator.EnableRunning;
            OnRunEnded -= _characterAnimator.DisableRunning;
        }

        #endregion

        public void TryMove(Vector3 movement)
        {
            movement = UpdateSpeed(movement, _speed);
            if (movement.x == 0 && movement.z == 0)
            {
                OnRunEnded?.Invoke();
                return;
            }

            OnRunStarted?.Invoke();
            
            ApplyBodyDirection(movement);

            movement = ApplyGravity(movement);
            Move(movement);
        }

        private Vector3 UpdateSpeed(Vector3 movement, float speed)
        {
            movement *= speed;
            movement = Vector3.ClampMagnitude(movement, speed);
            return movement;
        }

        private void ApplyBodyDirection(Vector3 movement)
        {
            if (movement == Vector3.zero)
                return;

            var direction = Quaternion.LookRotation(movement);

            _transform.rotation = Quaternion.Lerp(
                _transform.rotation,
                direction,
                _rotationSpeed * Time.deltaTime);
        }

        private Vector3 ApplyGravity(Vector3 movement)
        {
            movement.y += Physics.gravity.y;
            return movement;
        }

        private void Move(Vector3 movement)
        {
            movement *= Time.deltaTime;
            _characterController.Move(movement);
        }
    }
}

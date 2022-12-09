using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace SaveTheGuy.Characters
{
    public class AgentMovement : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Character _character;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;

        [Header("Settings")]
        [SerializeField] private float _stopDistance = 3f;
        [SerializeField] [Range(0, 1.5f)] private float _movementBlockDuration = 1f;

        private MovementAnimator _characterAnimator;
        private AgentTarget _target;
        private Transform _transform;
        private LayerMask _targetLayer;
        private bool _isMovementBlocked = false;
        private readonly float _moveUpdateInterval = 0.1f;

        public event Action OnMovementStarted;
        public event Action OnMovementStopped;

        #region MonoBehaviour

        private void Awake()
        {
            InitFields();

            CheckIfIsOnNavMesh();

            StartCoroutine(Move());
        }

        private void OnEnable()
        {
            OnMovementStarted += _characterAnimator.EnableRunning;
            OnMovementStopped += _characterAnimator.DisableRunning;
        }
    
        private void OnDisable()
        {
            OnMovementStarted -= _characterAnimator.EnableRunning;
            OnMovementStopped -= _characterAnimator.DisableRunning;
        }

        #endregion

        public void SetTarget(AgentTarget target) => _target = target;

        public void ApplyTargetLayer() => _targetLayer = _target.gameObject.layer;

        public void ApplySpeed() => _navMeshAgent.speed = _character.Stats.MovementSpeed;

        private void InitFields()
        {
            _characterAnimator = new(_animator);
            _transform = transform;
        }

        private void CheckIfIsOnNavMesh()
        {
            if (!_navMeshAgent.isOnNavMesh)
                Destroy(gameObject);
        }

        private IEnumerator Move()
        {
            while (true)
            {
                TrySetDestination();
                TryInvokeMovementEvents();

                yield return new WaitForSeconds(_moveUpdateInterval);
            }
        }

        private void TrySetDestination()
        {
            TryStopInFrontOfTarget();

            if (!_target)
                return;

            var targetTransform = _target.transform;
            var targetPosition = targetTransform.position;
            _navMeshAgent.SetDestination(targetPosition);
        }

        private void TryStopInFrontOfTarget()
        {
            var raycast = Physics.Raycast(_transform.position, _transform.forward, out var hit);
            if (!raycast)
                return;

            if (_isMovementBlocked
                || hit.collider.gameObject.layer != _targetLayer
                || hit.distance >= _stopDistance)
                return;

            StartCoroutine(BlockMovement());
        }

        private IEnumerator BlockMovement()
        {
            _navMeshAgent.speed = 0f;
            _isMovementBlocked = true;

            yield return new WaitForSeconds(_movementBlockDuration);

            ApplySpeed();
            _isMovementBlocked = false;
        }

        private void TryInvokeMovementEvents()
        {
            if (_navMeshAgent.isStopped)
                OnMovementStopped?.Invoke();
            else
                OnMovementStarted?.Invoke();
        }
    }
}

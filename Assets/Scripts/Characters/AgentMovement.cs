using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Transform _hitSphere;
    [SerializeField] private float _hitRadius;
    [SerializeField] private float _stopDistance = 3f;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Transform _target;
    private Transform _transform;
    private LayerMask _targetLayer;
    private bool _isMovementBlocked = false;

    public event Action OnMovementStarted;
    public event Action OnMovementStopped;

    #region MonoBehaviour

    private void Awake()
    {
        if (!_navMeshAgent.isOnNavMesh)
            Destroy(gameObject);

        _transform = transform;
    }

    private void Update()
    {
        TrySetDestination();
        TryInvokeMovementEvents();
    }

    #endregion

    public void SetTarget(Transform target) => _target = target;

    public void ApplyTargetLayer() => _targetLayer = _target.gameObject.layer;

    public void ApplySpeed() => _navMeshAgent.speed = _character.Stats.MovementSpeed;

    private void TrySetDestination()
    {
        if (TryStopInFrontOfTarget()
            || !_target)
            return;

        _navMeshAgent.SetDestination(_target.position);
    }

    private bool TryStopInFrontOfTarget()
    {
        var raycast = Physics.Raycast(_transform.position, _transform.forward, out var hit);
        if (raycast)
            if (!_isMovementBlocked && hit.collider.gameObject.layer == _targetLayer && hit.distance < _stopDistance)
            {
                StartCoroutine(BlockMovement());
                return true;
            }

        return false;
    }

    private IEnumerator BlockMovement()
    {
        _navMeshAgent.speed = 0f;
        _isMovementBlocked = true;

        yield return new WaitForSeconds(1f);

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

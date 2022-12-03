using UnityEngine;

public class CharacterAnimator
{
    public const string Running = nameof(Running);
    public const string Jumped = nameof(Jumped);

    private Animator _animator;

    public CharacterAnimator(Animator animator) => _animator = animator;

    public void EnableRunning() => _animator.SetBool(Running, true);

    public void DisableRunning() => _animator.SetBool(Running, false);
}
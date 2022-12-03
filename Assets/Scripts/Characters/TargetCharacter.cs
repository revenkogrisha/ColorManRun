using System;

public class TargetCharacter : CharacterAwakeInit
{
    public event Action OnTargetDied;

    protected override void Die()
    {
        OnTargetDied?.Invoke();
        base.Die();
    }
}

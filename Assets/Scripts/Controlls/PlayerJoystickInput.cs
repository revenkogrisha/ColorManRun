using UnityEngine;

public class PlayerJoystickInput
{
    private Joystick _joystick;
    private RelativeMovement _movement;

    public PlayerJoystickInput(Joystick joystick, RelativeMovement movement)
    {
        _joystick = joystick;
        _movement = movement;
    }

    public void Move()
    {
        var movement = GetMovementVector();
        _movement.TryMove(movement);
    }

    private Vector3 GetMovementVector() => new Vector3(
        _joystick.Horizontal,
        0,
        _joystick.Vertical);
}

using UnityEngine;

namespace ColorManRun.Control
{
    public class PlayerInput
    {
        public Vector3 GetMovementVector(Joystick joystick)
        {
            var horizontal = joystick.Horizontal;
            return new(horizontal, 0f, 1f);
        }

        public Vector3 GetMovementVector(Joystick joystick, float forwardSpeed, float horizontalSpeed)
        {
            var horizontal = joystick.Horizontal;
            return new(
                horizontal * horizontalSpeed,
                0f,
                forwardSpeed);
        }
    }
}

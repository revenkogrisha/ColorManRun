using UnityEngine;

namespace SaveTheGuy.Characters
{
    public interface IMoveable
    {
        public void TryMove(Vector3 movement);
    }
}

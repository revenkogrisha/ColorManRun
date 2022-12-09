using UnityEngine;

namespace SaveTheGuy.Level
{
    public class GroundCheck
    {
        private readonly LayerMask _groundLayer;

        public GroundCheck(LayerMask groundLayer)
        {
            _groundLayer = groundLayer;
        }

        public bool CheckGroundOnPosition(Vector3 position, float checkRadius) =>
            Physics.CheckSphere(position, checkRadius, _groundLayer);

        public bool CheckGroundOnPosition(Vector3 position, float checkRadius, LayerMask groundLayer) =>
            Physics.CheckSphere(position, checkRadius, groundLayer);
    }
}

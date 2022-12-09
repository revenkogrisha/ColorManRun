using UnityEngine;

namespace SaveTheGuy.Characters
{
    [CreateAssetMenu(fileName = "New Stats Config", menuName = "Configs/StatsConfig")]
    public class StatsConfig : ScriptableObject
    {
        [SerializeField] private float _healthAmount;
        [SerializeField] private float _damageAmount;
        [SerializeField] private float _damageResistance;
        [SerializeField] private float _stunDuration;
        [SerializeField] private float _movementSpeed;

        public float HealthAmount => _healthAmount;
        public float DamageAmount => _damageAmount;
        public float DamageResistance => _damageResistance;
        public float StunDuration => _stunDuration;
        public float MovementSpeed => _movementSpeed;
    }
}

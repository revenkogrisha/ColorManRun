using System;
using UnityEngine;

namespace SaveTheGuy.Characters
{
    public class Character : MonoBehaviour
    {
        private Statistics _stats;

        public Statistics Stats => _stats;

        public event Action OnDamageTaken;
        public event Action OnCharacterDied;

        public void InitStats(StatsConfig statsConfig) => _stats = new(statsConfig);

        public void TakeDamage(float amount)
        {
            var damage = amount - _stats.DamageResistance;
            _stats.HealthAmount -= damage;

            if (_stats.HealthAmount <= 0)
            {
                Die();
                return;
            }

            OnDamageTaken?.Invoke();
        }

        protected void Die()
        {
            OnCharacterDied?.Invoke();
        }
    }
}

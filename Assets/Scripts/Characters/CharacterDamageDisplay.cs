using DG.Tweening;
using UnityEngine;

namespace SaveTheGuy.Characters
{
    public class CharacterDamageDisplay : MonoBehaviour
    {
        [SerializeField] private Character _character;

        [Header("Scale")]
        [SerializeField] [Range(0f, 0.3f)] private float _scaleDuration;
        [SerializeField] [Range(0.5f, 1f)] private float _scaleAmount;
        [SerializeField] [Range(0f, 1)] private float _displayDurationInSeconds;

        private float _destroyDelay = 1f;

        #region MonoBehaviour

        private void OnEnable()
        {
            _character.OnDamageTaken += DisplayDamage;
            _character.OnCharacterDied += DisplayDeath;
        }

        private void OnDisable()
        {
            _character.OnDamageTaken -= DisplayDamage;
            _character.OnCharacterDied -= DisplayDeath;
        }

        #endregion

        public void DisplayDamage()
        {
            DisplayScale();
        }

        private void DisplayScale()
        {
            DOTween.Sequence()
                .Append(transform.DOScale(.8f, _scaleDuration))
                .Append(transform.DOScale(1f, _scaleDuration));
        }

        private void DisplayDeath()
        {
            DOTween.Sequence()
                .Append(transform.DOScale(0f, _scaleDuration))
                .AppendInterval(_destroyDelay)
                .AppendCallback(ApplyDeath);
        }

        private void ApplyDeath()
        {
            Destroy(gameObject);
        }
    }
}

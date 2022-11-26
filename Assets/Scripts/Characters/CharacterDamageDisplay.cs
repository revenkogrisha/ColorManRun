using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterDamageDisplay : MonoBehaviour
{
    [SerializeField] private Character _character;

    [Header("Color")]
    [SerializeField] private bool _displayColor = false;
    [SerializeField] private MeshRenderer[] _renderers;
    [SerializeField] private Color _damageColor;

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

        if (_displayColor)
            StartCoroutine(DisplayColor());
    }

    private void DisplayScale()
    {
        DOTween.Sequence()
            .Append(transform.DOScale(.8f, _scaleDuration))
            .Append(transform.DOScale(1f, _scaleDuration));
    }

    private IEnumerator DisplayColor()
    {
        var originalColors = GetOriginalColors(_renderers);

        foreach (var renderer in _renderers)
            renderer.material.color = _damageColor;

        yield return new WaitForSeconds(_displayDurationInSeconds);

        for (var i = 0; i < _renderers.Length; i++)
            _renderers[i].material.color = originalColors[i];
    }

    private Color[] GetOriginalColors(MeshRenderer[] renderers)
    {
        var originalColors = new List<Color>();

        for (var i = 0; i < renderers.Length; i++)
            originalColors.Add(renderers[i].material.color);

        return originalColors.ToArray();
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

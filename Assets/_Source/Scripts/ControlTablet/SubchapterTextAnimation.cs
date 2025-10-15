using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubchapterTextAnimation : MonoBehaviour
{

    #region Inner Classes
    
    [Serializable]
    private class State
    {
        [SerializeField] private float _textKegl;
        [SerializeField] private float _textXPosition;
        [SerializeField] private Vector2 _backSize;
        [SerializeField] private Color _backColor;

        private Sequence _sequence;

        public float TextKegl => _textKegl;
        public float TextXPosition => _textXPosition;
        public Vector2 BackSize => _backSize;
        public Color BackColor => _backColor;

        public Tween Set(TMP_Text text, Graphic back, float duration)
        {
            _sequence?.Kill();

            _sequence = DOTween.Sequence();
            _sequence.Append(text.DOFontSize(_textKegl, duration));
            _sequence.Join(text.rectTransform.DOAnchorPosX(_textXPosition, duration));
            _sequence.Join(back.rectTransform.DOSizeDelta(_backSize, duration));
            _sequence.Join(back.DOColor(_backColor, duration));

            return _sequence;
        }
    }

    #endregion

    [SerializeField] private TMP_Text _name;
    [SerializeField] private Graphic _nameBack;
    [SerializeField] private State _defaultState;
    [SerializeField] private State _selectedState;

    [Space]
    [SerializeField] private float _transitionDuration;
    [SerializeField] private Ease _selectionEase;
    [SerializeField] private Ease _deselectionEase;

    public void Select() => _selectedState.Set(_name, _nameBack, _transitionDuration).SetEase(_selectionEase);
    public void Deselect() => _defaultState.Set(_name, _nameBack, _transitionDuration).SetEase(_deselectionEase);
}
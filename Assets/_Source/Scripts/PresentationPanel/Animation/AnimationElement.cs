using DG.Tweening;
using UnityEngine;

namespace Animation
{
    public class AnimationElement : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private float _moveDuration;
        [SerializeField] private float _xStartOffset;

        private float _originalAnchoredX;
        private Sequence _sequence;

        private void Awake()
        {
            _originalAnchoredX = _rectTransform.anchoredPosition.x;
            _canvasGroup.alpha = 0f;
        }

        public Tween Show()
        {
            var startPos = _rectTransform.anchoredPosition;
            startPos.x = _originalAnchoredX + _xStartOffset;
            _rectTransform.anchoredPosition = startPos;

            if (_canvasGroup != null)
                _canvasGroup.alpha = 0f;

            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            
            if (_canvasGroup != null)
                _sequence.Append(_rectTransform.DOAnchorPosX(_originalAnchoredX, _moveDuration))
                    .Join(_canvasGroup.DOFade(1f, _fadeDuration));
            
            return _sequence;
        }
        
        public Tween Hide()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            if (_canvasGroup != null)
                _sequence.Append(_canvasGroup.DOFade(0f, _fadeDuration))
                    .Join(_rectTransform.DOAnchorPosX(_originalAnchoredX - _xStartOffset, _moveDuration));

            return _sequence;
        }

        public void ShowInstantly()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            
            _sequence.Append(_canvasGroup.DOFade(1f, 0f));
        }
    }
}
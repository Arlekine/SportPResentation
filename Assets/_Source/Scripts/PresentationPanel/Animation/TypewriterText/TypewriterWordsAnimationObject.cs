using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Animation.TypewriterText
{
    public sealed class TypewriterWordsAnimationObject : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private float _wordsPerSecond = 10f;
        [SerializeField] private bool _useUnscaledTime = false;
        
        [Space, Header("Canvas group settings")] 
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuraction;

        private Tween _tween;
        private int _currentVisible;

        public Tween Show()
        {
            if (_label == null)
                return DOVirtual.DelayedCall(0f, () => { });

            _tween?.Kill();
            _label.ForceMeshUpdate();

            var total = _label.textInfo.wordCount;

            if (total <= 0)
                return DOVirtual.DelayedCall(0f, () => { }).SetUpdate(_useUnscaledTime);

            if (_wordsPerSecond <= 0f)
            {
                _label.maxVisibleWords = total;
                return DOVirtual.DelayedCall(0f, () => { }).SetUpdate(_useUnscaledTime);
            }

            _currentVisible = 0;
            _label.maxVisibleWords = 0;
            var duration = total / _wordsPerSecond;

            _tween = DOVirtual.Int(0, total, duration, v =>
                {
                    if (v == _currentVisible) return;
                    _currentVisible = v;
                    _label.maxVisibleWords = v;
                })
                .SetEase(Ease.Linear)
                .SetUpdate(_useUnscaledTime)
                .OnComplete(() =>
                {
                    _currentVisible = total;
                    _label.maxVisibleWords = total;
                });

            return _tween;
        }

        public Tween Hide() =>
            _canvasGroup.DOFade(0f, _fadeDuraction);

        public Tween HideInstantly() =>
            _canvasGroup.DOFade(0f, 0f);
        
        public Tween ShowInstantly() =>
            _canvasGroup.DOFade(1f, 0f);

        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}
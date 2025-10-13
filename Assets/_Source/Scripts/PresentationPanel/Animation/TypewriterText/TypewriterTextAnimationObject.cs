using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Animation.TypewriterText
{
    public sealed class TypewriterTextAnimationObject : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private float _charsPerSecond = 100f;
        [SerializeField] private bool _useUnscaledTime = false;

        private Tween _tween;
        private int _currentVisible;
        
        public Tween Show()
        {
            if (_label == null) return DOVirtual.DelayedCall(0f, () => { });
            _tween?.Kill();
            _label.ForceMeshUpdate();
            var total = _label.textInfo.characterCount;
            if (total <= 0) return DOVirtual.DelayedCall(0f, () => { }).SetUpdate(_useUnscaledTime);
            if (_charsPerSecond <= 0f)
            {
                _label.maxVisibleCharacters = total;
                return DOVirtual.DelayedCall(0f, () => { }).SetUpdate(_useUnscaledTime);
            }
            _currentVisible = 0;
            _label.maxVisibleCharacters = 0;
            var duration = total / _charsPerSecond;
            _tween = DOVirtual.Int(0, total, duration, v =>
                {
                    if (v == _currentVisible) return;
                    _currentVisible = v;
                    _label.maxVisibleCharacters = v;
                })
                .SetEase(Ease.Linear)
                .SetUpdate(_useUnscaledTime)
                .OnComplete(() =>
                {
                    _currentVisible = total;
                    _label.maxVisibleCharacters = total;
                });
            return _tween;
        }
        
        public Tween Hide()
        {
            if (_label == null) return DOVirtual.DelayedCall(0f, () => { });
            _tween?.Kill();
            _label.ForceMeshUpdate();
            var total = _label.textInfo.characterCount;
            var start = Mathf.Min(_label.maxVisibleCharacters, total);
            if (total <= 0 || start <= 0)
            {
                _label.maxVisibleCharacters = 0;
                return DOVirtual.DelayedCall(0f, () => { }).SetUpdate(_useUnscaledTime);
            }
            if (_charsPerSecond <= 0f)
            {
                _label.maxVisibleCharacters = 0;
                return DOVirtual.DelayedCall(0f, () => { }).SetUpdate(_useUnscaledTime);
            }
            _currentVisible = start;
            var duration = start / _charsPerSecond;
            _tween = DOVirtual.Int(start, 0, duration, v =>
                {
                    if (v == _currentVisible) return;
                    _currentVisible = v;
                    _label.maxVisibleCharacters = v;
                })
                .SetEase(Ease.Linear)
                .SetUpdate(_useUnscaledTime)
                .OnComplete(() =>
                {
                    _currentVisible = 0;
                    _label.maxVisibleCharacters = 0;
                });
            return _tween;
        }
        
        private void OnDisable()
        {
            _tween?.Kill();
        }
    }
}
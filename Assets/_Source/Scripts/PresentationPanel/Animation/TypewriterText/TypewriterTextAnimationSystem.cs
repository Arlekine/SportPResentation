using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using com.cyborgAssets.inspectorButtonPro;

namespace Animation.TypewriterText
{
    public sealed class TypewriterTextAnimationSystem : MonoBehaviour
    {
        [SerializeField] private List<TypewriterTextAnimationObject> _typewriterTextAnimationObjects;
        [SerializeField] private bool _hideReverseOrder = true;
        [SerializeField] private bool _useUnscaledTime = false;
        [SerializeField] private bool _hideOnAwake = true;

        private Tween _systemTween;

        private void Awake()
        {
            if (_hideOnAwake)
                HideOnAwake();
        }

        [ProButton]
        public Tween Show()
        {
            _systemTween?.Kill();
            if (_typewriterTextAnimationObjects == null || _typewriterTextAnimationObjects.Count == 0) 
                return DOVirtual.DelayedCall(0f, () => { }).SetUpdate(_useUnscaledTime);

            var sequence = DOTween.Sequence().SetUpdate(_useUnscaledTime);
            foreach (var obj in _typewriterTextAnimationObjects)
            {
                if (obj == null) continue;
                sequence.Append(obj.Show());
            }
            _systemTween = sequence;
            return _systemTween;
        }

        [ProButton]
        public Tween Hide()
        {
            _systemTween?.Kill();
            if (_typewriterTextAnimationObjects == null || _typewriterTextAnimationObjects.Count == 0)
                return DOVirtual.DelayedCall(0f, () => { }).SetUpdate(_useUnscaledTime);

            var sequence = DOTween.Sequence().SetUpdate(_useUnscaledTime);
            if (_hideReverseOrder)
            {
                for (var i = _typewriterTextAnimationObjects.Count - 1; i >= 0; i--)
                {
                    var obj = _typewriterTextAnimationObjects[i];
                    if (obj == null) continue;
                    sequence.Append(obj.Hide());
                }
            }
            else
            {
                foreach (var typewriterTextAnimationObject in _typewriterTextAnimationObjects)
                {
                    if (typewriterTextAnimationObject == null) 
                        continue;
                    sequence.Append(typewriterTextAnimationObject.Hide());
                }
            }
            _systemTween = sequence;
            return _systemTween;
        }

        private void HideOnAwake()
        {
            _systemTween?.Kill();
            if (_typewriterTextAnimationObjects == null) return;
            foreach (var obj in _typewriterTextAnimationObjects)
            {
                if (obj == null) continue;
                var hide = obj.Hide();
                if (hide != null && hide.IsActive()) 
                    hide.Complete(true);
            }
        }
        
        private void OnDisable()
        {
            _systemTween?.Kill();
        }
    }
}
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;

namespace Animation.TypewriterText
{
    public sealed class TypewriterWordsAnimationSystem : UIShowingAnimation
    {
        [SerializeField] private List<TypewriterWordsAnimationObject> _typewriterWordsAnimationObjects;
        
        [SerializeField] private bool _useUnscaledTime = false;
        [SerializeField] private bool _hideOnAwake = true;

        private Tween _systemTween;

        private void Awake()
        {
            if (_hideOnAwake)
                HideInstantly();
        }

        public override bool IsShowed { get; }

        [ProButton]
        public override Tween Show()
        {
            _systemTween?.Kill();
            
            if (_typewriterWordsAnimationObjects == null || _typewriterWordsAnimationObjects.Count == 0)
                return DOVirtual.DelayedCall(0f, () => { }).SetUpdate(_useUnscaledTime);
            
            foreach (var animationObject in _typewriterWordsAnimationObjects)
                animationObject.ShowInstantly();

            var sequence = DOTween.Sequence().SetUpdate(_useUnscaledTime);
            foreach (var obj in _typewriterWordsAnimationObjects)
            {
                if (obj == null) continue;
                sequence.Append(obj.Show());
            }
            
            _systemTween = sequence;
            
            return _systemTween;
        }

        [ProButton]
        public override Tween Hide()
        {
            _systemTween?.Kill();
            var sequence = DOTween.Sequence().SetUpdate(_useUnscaledTime);
            
            foreach (var textAnimationObject in _typewriterWordsAnimationObjects)
                sequence.Join(textAnimationObject.Hide());
            
            _systemTween = sequence;
            return _systemTween;
        }

        [ProButton]
        public override void ShowInstantly()
        {
            foreach (var obj in _typewriterWordsAnimationObjects)
                obj.ShowInstantly();
        }

        public override void HideInstantly()
        {
            foreach (var obj in _typewriterWordsAnimationObjects)
                obj.HideInstantly();
        }

        private void OnDisable()
        {
            _systemTween?.Kill();
        }
    }
}
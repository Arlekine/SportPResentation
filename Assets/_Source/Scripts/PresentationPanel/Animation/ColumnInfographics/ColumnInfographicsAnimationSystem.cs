using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;

namespace Animation.ColumnInfographics
{
    public class ColumnInfographicsAnimationSystem : UIShowingAnimation
    {
        [SerializeField] private List<ColumnAnimationObject> _columnAnimationObjects;
        [SerializeField] private float _pauseBetweenColumnsAnimation = 0.2f;

        private Sequence _sequence;

        public override bool IsShowed { get; }

        [ProButton]
        public override Tween Show()
        {
            _sequence?.Kill();

            _sequence = DOTween.Sequence();
            var start = 0f;

            foreach (var animationObject in _columnAnimationObjects)
            {
                var tween = animationObject.Show();

                _sequence.Insert(start, tween);
                start += _pauseBetweenColumnsAnimation;
            }

            return _sequence.Play();
        }

        [ProButton]
        public override Tween Hide()
        {
            _sequence?.Kill();

            _sequence = DOTween.Sequence();
            var start = 0f;

            for (var i = _columnAnimationObjects.Count - 1; i >= 0; i--)
            {
                var columnAnimationObject = _columnAnimationObjects[i];
                var tween = columnAnimationObject.Hide();

                _sequence.Insert(start, tween);
                start += _pauseBetweenColumnsAnimation;
            }

            return _sequence.Play();
        }

        [ProButton]
        public override void ShowInstantly()
        {
            foreach (var animationObject in _columnAnimationObjects)
                animationObject.ShowInstantly();
        }

        [ProButton]
        public override void HideInstantly()
        {
            foreach (var animationObject in _columnAnimationObjects)
                animationObject.HideInstantly();
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
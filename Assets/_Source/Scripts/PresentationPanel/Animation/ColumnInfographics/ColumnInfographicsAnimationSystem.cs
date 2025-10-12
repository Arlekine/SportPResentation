using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;

namespace Animation.ColumnInfographics
{
    public class ColumnInfographicsAnimationSystem : MonoBehaviour
    {
        [SerializeField] private List<ColumnAnimationObject> _columnAnimationObjects = new();
        [SerializeField] private float _pauseBetweenColumnsAnimation = 0.2f;

        private Sequence _sequence;

        [ProButton]
        public void Show()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            if (_columnAnimationObjects == null || _columnAnimationObjects.Count == 0)
                return;

            var delay = 0f;
            foreach (var columnAnimationObject in _columnAnimationObjects)
            {
                if (columnAnimationObject == null) 
                    continue;

                var tween = columnAnimationObject.Show();
                if (tween == null) 
                    continue;

                tween.SetDelay(delay);
                _sequence.Join(tween);
                delay += _pauseBetweenColumnsAnimation;
            }

            _sequence.Play();
        }

        [ProButton]
        public void Hide()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            if (_columnAnimationObjects == null || _columnAnimationObjects.Count == 0)
                return;

            var delay = 0f;
            for (var i = _columnAnimationObjects.Count - 1; i >= 0; i--)
            {
                var columnAnimationObject = _columnAnimationObjects[i];
                if (columnAnimationObject == null) continue;

                var tween = columnAnimationObject.Hide();
                if (tween == null) continue;

                tween.SetDelay(delay);
                _sequence.Join(tween);
                delay += _pauseBetweenColumnsAnimation;
            }

            _sequence.Play();
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
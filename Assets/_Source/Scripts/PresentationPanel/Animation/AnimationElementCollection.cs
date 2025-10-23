using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Animation
{
    public class AnimationElementCollection : MonoBehaviour
    {
        [SerializeField] private List<AnimationElement> _animationElements;
        [SerializeField] private float _delay;
        
        private Sequence _sequence;
        
        [ProButton]
        public async UniTask Show()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            foreach (var element in _animationElements)
            {
                element.Show();
                await UniTask.WaitForSeconds(_delay);
            }
        }
        
        [ProButton]
        public async UniTask Hide()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            foreach (var element in _animationElements)
            {
                element.Hide();
                await UniTask.WaitForSeconds(_delay);
            }
        }

        private void OnDestroy() =>
            _sequence?.Kill();
    }
}
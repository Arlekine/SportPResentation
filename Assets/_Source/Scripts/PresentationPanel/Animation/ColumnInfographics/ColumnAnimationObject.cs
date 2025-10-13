using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animation.ColumnInfographics
{
    public class ColumnAnimationObject : MonoBehaviour
    {
        [SerializeField] private RectTransform _columnTransform;
        [SerializeField] private float _animationTime;
        [SerializeField] private float _yFinalSize;
        
        private float _yStartSize;
        
        private void Awake()
        {
            _yStartSize = _columnTransform.sizeDelta.y;
        }

        public Tween Show() =>
            _columnTransform.DOSizeDelta(new Vector2(_columnTransform.sizeDelta.x, _yFinalSize), _animationTime);
        
        public Tween Hide() =>
            _columnTransform.DOSizeDelta(new Vector2(_columnTransform.sizeDelta.x, _yStartSize), _animationTime);

        public Tween ShowInstantly() =>
            _columnTransform.DOSizeDelta(new Vector2(_columnTransform.sizeDelta.x, _yFinalSize), 0f);

        public Tween HideInstantly() =>
            _columnTransform.DOSizeDelta(new Vector2(_columnTransform.sizeDelta.x, _yStartSize), 0f);
    }
}
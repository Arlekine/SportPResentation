using DG.Tweening;
using UnityEngine;

namespace Animation.ColumnInfographics
{
    public class ColumnAnimationObject : MonoBehaviour
    {
        [SerializeField] private RectTransform _columnTransform;
        [SerializeField] private float _animationTime;
        [SerializeField] private float _yFinalPosition;
        
        private float _yStartPosition;
        private Sequence _sequence;
        
        private void Awake()
        {
            _yStartPosition = _columnTransform.anchoredPosition.y;
        }
        
        public Tween Show() =>
            _sequence.Append(_columnTransform.DOAnchorPosY(_yFinalPosition, _animationTime));
        
        public Tween Hide() =>
            _sequence.Append(_columnTransform.DOAnchorPosY(_yStartPosition, _animationTime));

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}
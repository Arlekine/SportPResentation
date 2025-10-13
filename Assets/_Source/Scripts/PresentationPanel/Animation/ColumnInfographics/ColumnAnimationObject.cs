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
        
        private void Awake()
        {
            _yStartPosition = _columnTransform.anchoredPosition.y;
        }
        
        public Tween Show() =>
            _columnTransform.DOAnchorPosY(_yFinalPosition, _animationTime);
        
        public Tween Hide() =>
            _columnTransform.DOAnchorPosY(_yStartPosition, _animationTime);
    }
}
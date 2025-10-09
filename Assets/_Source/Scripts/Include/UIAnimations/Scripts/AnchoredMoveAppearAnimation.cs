using DG.Tweening;
using UnityEngine;

public abstract class AnchoredMoveAppearAnimation : UIShowingAnimation
{
    public abstract Vector2 ShowPosition { get; }
    public abstract Vector2 HidePosition { get; }

    [SerializeField] protected RectTransform _panel;
    [SerializeField] protected float _moveTime;
    [SerializeField] protected Ease _moveEase;
    [SerializeField] protected bool _hideBeforeShow;

    private bool _isShowed;
    private Sequence _moveSequence;

    public override bool IsShowed => _isShowed;
    
    public override Tween Show()
    {
        if (_hideBeforeShow)
            HideInstantly();

        _isShowed = true;
        _moveSequence?.Kill();
        _moveSequence = DOTween.Sequence();

        _moveSequence.Append(_panel.DOAnchorPos(ShowPosition, _moveTime).SetEase(_moveEase));
        return _moveSequence;
    }

    public override Tween Hide()
    {
        _isShowed = false;
        _moveSequence?.Kill();
        _moveSequence = DOTween.Sequence();

        _moveSequence.Append(_panel.DOAnchorPos(HidePosition, _moveTime).SetEase(_moveEase));
        return _moveSequence;
    }

    public override void ShowInstantly()
    {
        _isShowed = true;
        _panel.anchoredPosition = ShowPosition;
    }

    public override void HideInstantly()
    {
        _isShowed = false;
        _panel.anchoredPosition = HidePosition;
    }
}
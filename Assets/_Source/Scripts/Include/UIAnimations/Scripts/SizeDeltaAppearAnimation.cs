using System;
using DG.Tweening;
using UnityEngine;

public class SizeDeltaAppearAnimation : UIShowingAnimation
{
    [SerializeField] protected Vector2 _showSizeDelta;
    [SerializeField] protected Vector2 _hideSizeDelta;
    [SerializeField] protected RectTransform _panel;
    [SerializeField] protected float _moveTime;
    [SerializeField] protected Ease _moveEase;

    private bool _isShowed;
    private Sequence _moveSequence;

    public override bool IsShowed => _isShowed;

    public override Tween Show()
    {
        _isShowed = true;
        _moveSequence?.Kill();
        _moveSequence = DOTween.Sequence();

        _moveSequence.Append(_panel.DOSizeDelta(_showSizeDelta, _moveTime).SetEase(_moveEase));
        return _moveSequence;
    }

    public override Tween Hide()
    {
        _isShowed = false;
        _moveSequence?.Kill();
        _moveSequence = DOTween.Sequence();

        _moveSequence.Append(_panel.DOSizeDelta(_hideSizeDelta, _moveTime).SetEase(_moveEase));
        return _moveSequence;
    }

    public override void ShowInstantly()
    {
        _isShowed = true;
        _panel.sizeDelta = _showSizeDelta;
    }

    public override void HideInstantly()
    {
        _isShowed = false;
        _panel.sizeDelta = _hideSizeDelta;
    }
}
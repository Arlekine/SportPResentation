using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GraphicColorShowingAnimation : UIShowingAnimation
{
    [SerializeField] private Graphic[] _graphics;
    [SerializeField] private Color _showColor = Color.white;
    [SerializeField] private Color _hideColor = Color.white;
    [SerializeField] private float _animationTime = 0.3f;

    public override bool IsShowed { get; }

    private bool _isShowed;

    public override Tween Show()
    {
        _isShowed = true;

        var sequence = DOTween.Sequence();
        foreach (var graphic in _graphics)
        {
            sequence.Join(graphic.DOColor(_showColor, _animationTime));
        }

        return sequence;
    }

    public override Tween Hide()
    {
        _isShowed = false;

        var sequence = DOTween.Sequence();
        foreach (var graphic in _graphics)
        {
            sequence.Join(graphic.DOColor(_hideColor, _animationTime));
        }

        return sequence;
    }

    public override void ShowInstantly()
    {
        _isShowed = true;
        foreach (var graphic in _graphics)
        {
            graphic.color = _showColor;
        }
    }

    public override void HideInstantly()
    {
        _isShowed = false;
        foreach (var graphic in _graphics)
        {
            graphic.color = _hideColor;
        }
    }
}
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GraphicAlphaAnimation : UIShowingAnimation
{
    [SerializeField] protected Graphic[] _graphics;
    [SerializeField] protected float _showTime = 0.3f;

    public override bool IsShowed => _graphics[0].color.a > 0;

    public override Tween Show()
    {
        var sequence = DOTween.Sequence();

        foreach (var graphic in _graphics)
        {
            sequence.Join(graphic.DOFade(1f, _showTime));
        }

        return sequence;
    }

    public override Tween Hide()
    {
        var sequence = DOTween.Sequence();

        foreach (var graphic in _graphics)
        {
            sequence.Join(graphic.DOFade(0f, _showTime));
        }

        return sequence;
    }

    public override void ShowInstantly()
    {
        foreach (var graphic in _graphics)
        {
            graphic.SetAlpha(1f);
        }
    }

    public override void HideInstantly()
    {
        foreach (var graphic in _graphics)
        {
            graphic.SetAlpha(0f);
        }
    }
}
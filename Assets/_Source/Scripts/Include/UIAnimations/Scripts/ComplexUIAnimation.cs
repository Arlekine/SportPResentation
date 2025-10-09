using DG.Tweening;
using UnityEngine;


public class ComplexUIAnimation : UIShowingAnimation
{
    [SerializeField] private UIShowingAnimation[] _animations;
    
    public override bool IsShowed {
        get 
        {
            foreach (var showingAnimation in _animations)
            {
                if (showingAnimation.IsShowed == false)
                    return false;
            }

            return true;
        }
    }

    public override Tween Show()
    {
        var sequence = DOTween.Sequence();
        foreach (var uiShowingAnimation in _animations)
        {
            sequence.Join(uiShowingAnimation.Show());
        }

        return sequence;
    }

    public override Tween Hide()
    {
        var sequence = DOTween.Sequence();
        foreach (var uiShowingAnimation in _animations)
        {
            sequence.Join(uiShowingAnimation.Hide());
        }

        return sequence;
    }

    public override void ShowInstantly()
    {
        foreach (var uiShowingAnimation in _animations)
        {
            uiShowingAnimation.ShowInstantly();
        }
    }

    public override void HideInstantly()
    {
        foreach (var uiShowingAnimation in _animations)
        {
            uiShowingAnimation.HideInstantly();
        }
    }
}
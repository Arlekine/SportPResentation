using System;
using DG.Tweening;
using UnityEngine;

public class UIAnimationSystem : UIShowingAnimation
{
    [Serializable]
    private class AnimationStep
    {
        [SerializeField] private UIShowingAnimation[] _animations;
        [SerializeField] private float _stepDelay;

        public Tween Show(bool preHide = false)
        {
            var sequence = DOTween.Sequence();
            foreach (var uiShowingAnimation in _animations)
            {
                if (preHide)
                    uiShowingAnimation.HideInstantly();

                sequence.Join(uiShowingAnimation.Show().SetDelay(_stepDelay));
            }

            return sequence;
        }

        public void ShowInstantly()
        {
            foreach (var uiShowingAnimation in _animations)
                uiShowingAnimation.ShowInstantly();
        }

        public Tween Hide()
        {
            var sequence = DOTween.Sequence();
            foreach (var uiShowingAnimation in _animations)
                sequence.Join(uiShowingAnimation.Hide().SetDelay(_stepDelay));
            return sequence;
        }

        public void HideInstantly()
        {
            foreach (var uiShowingAnimation in _animations)
                uiShowingAnimation.HideInstantly();
        }
    }

    [SerializeField] private AnimationStep[] _showAnimations;
    [SerializeField] private AnimationStep[] _hideAnimations;
    [SerializeField] private bool _preHide;

    private Sequence _currentAnimation;
    private bool _isShowed;

    public override bool IsShowed => _isShowed;

    public override Tween Show()
    {
        _currentAnimation?.Kill();
        _currentAnimation = DOTween.Sequence();

        foreach (var animation in _showAnimations)
            _currentAnimation.Join(animation.Show(_preHide));

        return _currentAnimation;
    }

    public override Tween Hide()
    {
        _currentAnimation?.Kill();
        _currentAnimation = DOTween.Sequence();

        foreach (var animation in _hideAnimations)
            _currentAnimation.Join(animation.Hide());

        return _currentAnimation;
    }

    public override void ShowInstantly()
    {
        foreach (var animation in _showAnimations)
            animation.ShowInstantly();

        foreach (var animation in _hideAnimations)
            animation.ShowInstantly();
    }

    public override void HideInstantly()
    {
        foreach (var animation in _showAnimations)
            animation.HideInstantly();

        foreach (var animation in _hideAnimations)
            animation.HideInstantly();
    }
}
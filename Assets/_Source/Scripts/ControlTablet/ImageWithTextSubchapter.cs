using System;
using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ImageWithTextSubchapter : MonoBehaviour, ISubchapterView
{
    private const float BACK_BUTTON_AREA_DEFAULT_X = 0f;

    [SerializeField] private Button _button;
    [SerializeField] private SubchapterTextAnimation _subchapterTextAnimation;

    [Space]
    [SerializeField] private RectTransform _backButtonArea;
    [SerializeField] private float _backButtonAreaMoveDuration;
    [SerializeField] private Ease _backButtonAreaMoveEase;

    public event Action<ISubchapterView> Clicked;

    public RectTransform RectTransform => (RectTransform)transform;

    [ProButton]
    public void Select()
    {
        _subchapterTextAnimation.Select();
        _backButtonArea.DOAnchorPosX(_backButtonArea.sizeDelta.x, _backButtonAreaMoveDuration).SetEase(_backButtonAreaMoveEase);
    }

    [ProButton]
    public void Deselect()
    {
        _subchapterTextAnimation.Deselect();
        _backButtonArea.DOAnchorPosX(BACK_BUTTON_AREA_DEFAULT_X, _backButtonAreaMoveDuration).SetEase(_backButtonAreaMoveEase);
    }
}
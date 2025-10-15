using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;

public class BackSwitcher : MonoBehaviour
{
    [SerializeField] private RectTransform _blueBack;
    [SerializeField] private float _regionsY;
    [SerializeField] private float _chaptersY;

    [Space]
    [SerializeField] private float _moveTime;
    [SerializeField] private AnimationCurve _ease;

    [ProButton] public void SetRegionsState() => _blueBack.DOAnchorPosY(_regionsY, _moveTime).SetEase(_ease);
    [ProButton] public void SetChaptersState() => _blueBack.DOAnchorPosY(_chaptersY, _moveTime).SetEase(_ease);
}
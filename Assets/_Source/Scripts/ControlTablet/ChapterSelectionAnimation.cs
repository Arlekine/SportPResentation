using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSelectionAnimation : SelectionAnimation
{
    [SerializeField] private Graphic _line;
    [SerializeField] private RectTransform _mainPart;
    [SerializeField] private RectTransform _subChaptersPart;
    [SerializeField] private RectTransform _root;
    [SerializeField] private LayoutElement _rootLayoutElement;
    [SerializeField] private float _animationDuration;

    private Sequence _currentMainSequence;

    public override void Select()
    {
        _mainPart.anchorMin = Vector2.up;
        _mainPart.anchorMax = Vector2.one;

        _subChaptersPart.anchorMin = Vector2.up;
        _subChaptersPart.anchorMax = Vector2.one;

        _subChaptersPart.anchoredPosition = new Vector2(_subChaptersPart.anchoredPosition.x, -_root.sizeDelta.y);
        
        _currentMainSequence?.Kill();
        _currentMainSequence = DOTween.Sequence();

        _currentMainSequence.Append(_line.DOFade(1f, _animationDuration));
        _currentMainSequence.Join(_rootLayoutElement.DOPrefferedHeight(_subChaptersPart.sizeDelta.y, _animationDuration));
    }

    public override void Deselect()
    {
        _currentMainSequence?.Kill();
        _currentMainSequence = DOTween.Sequence();

        _currentMainSequence.Append(_line.DOFade(0f, _animationDuration));
        _currentMainSequence.Join(_rootLayoutElement.DOPrefferedHeight(0f, _animationDuration));
    }

    public override void SelectInstantly()
    {
        _mainPart.anchorMin = Vector2.up;
        _mainPart.anchorMax = Vector2.one;

        _subChaptersPart.anchorMin = Vector2.up;
        _subChaptersPart.anchorMax = Vector2.one;

        _line.SetAlpha(1f);
        _rootLayoutElement.preferredHeight = _subChaptersPart.sizeDelta.y;
    }

    public override void DeselectInstantly()
    {
        _line.SetAlpha(0f);
        _rootLayoutElement.preferredHeight = 0f;
    }
}
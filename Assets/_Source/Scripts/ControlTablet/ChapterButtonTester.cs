using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;

public class ChapterButtonTester : MonoBehaviour
{
    [SerializeField] private ChapterButton _chapterButton;
    [SerializeField] private RectTransform _scrollContent;
    [SerializeField] private ImageWithTextSubchapter[] _subchapters;

    private Vector2 _standartSize;

    [ProButton]
    public void AddTestSubchapters()
    {
        _standartSize = _scrollContent.sizeDelta;
        _chapterButton.SetSubchapters(_subchapters);
    }

    [ProButton]
    public void Select()
    {
        _chapterButton.Select();
        var scrollContentTargetSize = _standartSize;
        scrollContentTargetSize.y += _chapterButton.OpenedAdditionalSize;
        _scrollContent.DOSizeDelta(scrollContentTargetSize, 0.3f);
    }

    [ProButton]
    public void Deselect()
    {
        _chapterButton.Deselect();
        _scrollContent.DOSizeDelta(_standartSize, 0.3f);
    }
}
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ControlTabletUIRoot : MonoBehaviour
{
    [SerializeField] private IdButtonsGroup _regionsButtonsGroup;
    [SerializeField] private IdButtonsGroup _chapterButtonsGroup;
    [SerializeField] private Button _chapterBackButton;

    [Space]
    [SerializeField] private Vector2 _regionsHidePosition;
    [SerializeField] private Vector2 _mainPosition;
    [SerializeField] private Vector2 _chaptersHidePosition;
    [SerializeField] private float _animationTime;

    private string _currentRegion;

    public event Action<string, string> ChapterChanged;

    private void OnEnable()
    {
        _regionsButtonsGroup.Clicked += OnRegionChanged;
        _chapterButtonsGroup.Clicked += OnChapterChanged;
        _chapterBackButton.onClick.AddListener(OnBack);
    }

    private void OnDisable()
    {
        _regionsButtonsGroup.Clicked -= OnRegionChanged;
        _chapterButtonsGroup.Clicked -= OnChapterChanged;
        _chapterBackButton.onClick.RemoveListener(OnBack);
    }

    private void OnRegionChanged(string regionID)
    {
        _currentRegion = regionID;

        _regionsButtonsGroup.GetComponent<RectTransform>().DOAnchorPos(_regionsHidePosition, _animationTime);
        _chapterButtonsGroup.GetComponent<RectTransform>().DOAnchorPos(_mainPosition, _animationTime);
    }

    private void OnChapterChanged(string chapterID)
    {
        ChapterChanged?.Invoke(_currentRegion, chapterID);
    }

    private void OnBack()
    {
        _regionsButtonsGroup.GetComponent<RectTransform>().DOAnchorPos(_mainPosition, _animationTime);
        _chapterButtonsGroup.GetComponent<RectTransform>().DOAnchorPos(_chaptersHidePosition, _animationTime);
    }
}
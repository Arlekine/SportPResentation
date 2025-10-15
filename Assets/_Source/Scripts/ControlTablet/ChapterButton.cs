using System;
using System.Collections.Generic;
using System.Linq;
using com.cyborgAssets.inspectorButtonPro;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ChapterButton : IDButton
{
    [SerializeField][ID("Chapters")] private string _chapterID;
    [SerializeField] private GridLayoutGroup _subchaptersParent;

    [Space]
    [SerializeField] private SelectionAnimation _hasSubchapterAnimation;
    [SerializeField] private SelectionAnimation _noSubchapterAnimation;

    private List<ISubchapterView> _subchapters;
    private ISubchapterView _selectedSubchapter;

    public event Action<int> SubchapterSelected; 
    public event Action SubchapterDeselected;

    public override string ID => _chapterID;
    public float OpenedAdditionalSize => SubchaptersParentParent.sizeDelta.y;

    private RectTransform SubchaptersParentParent => (RectTransform)_subchaptersParent.transform.parent;
    private RectTransform SubchaptersParent=> (RectTransform)_subchaptersParent.transform;

    [ProButton]
    public override void Select()
    {
        _noSubchapterAnimation.Select();

        if (_subchapters != null)
            _hasSubchapterAnimation.Select();
    }

    [ProButton]
    public override void Deselect()
    {
        _noSubchapterAnimation.Deselect();

        if (_subchapters != null)
            _hasSubchapterAnimation.Deselect();
    }
    
    public async void SetSubchapters(IEnumerable<ISubchapterView> subchapters)
    {
        _subchapters = subchapters.ToList();

        foreach (var subchapterView in _subchapters)
        {
            subchapterView.RectTransform.SetParent(_subchaptersParent.transform);
            subchapterView.Clicked += OnSubchapterClicked;
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(SubchaptersParent);

        var prefferedSize = LayoutUtility.GetPreferredHeight(SubchaptersParent)  + (-SubchaptersParent.offsetMax.y + SubchaptersParent.offsetMin.y);
        SubchaptersParentParent.sizeDelta = new Vector2(SubchaptersParentParent.sizeDelta.x, prefferedSize);
    }

    private void OnSubchapterClicked(ISubchapterView subchapterView)
    {
        
        if (_selectedSubchapter != null)
            _selectedSubchapter.Deselect();

        if (_selectedSubchapter == subchapterView)
        {
            SubchapterDeselected?.Invoke();
            return;
        }

        _selectedSubchapter = subchapterView;
        _selectedSubchapter.Select();
        SubchapterSelected?.Invoke(_subchapters.IndexOf(subchapterView));
    }
}
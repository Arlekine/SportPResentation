using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSelectionAnimation : SelectionAnimation
{
    [SerializeField] private List<Graphic> _graphics;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _deselectedColor;
    [SerializeField] private float _switchTime;

    public override void Select() => _graphics.ForEach(x => x.DOColor(_selectedColor, _switchTime));
    public override void Deselect() => _graphics.ForEach(x => x.DOColor(_deselectedColor, _switchTime));
    public override void SelectInstantly() => _graphics.ForEach(x => x.color = _selectedColor);
    public override void DeselectInstantly() => _graphics.ForEach(x => x.color = _deselectedColor);
}
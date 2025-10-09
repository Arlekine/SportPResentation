using System.Collections.Generic;
using UnityEngine;

public class ComplexSelectionAnimation : SelectionAnimation
{
    [SerializeField] private List<SelectionAnimation> _selectionAnimations;

    public override void Select() => _selectionAnimations.ForEach(x => x.Select());
    public override void Deselect() => _selectionAnimations.ForEach(x => x.Deselect());
    public override void SelectInstantly() => _selectionAnimations.ForEach(x => x.SelectInstantly());
    public override void DeselectInstantly() => _selectionAnimations.ForEach(x => x.DeselectInstantly());
}
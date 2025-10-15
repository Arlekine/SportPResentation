using UnityEngine;

public abstract class SimpleIDButton : IDButton
{
    [SerializeField] protected SelectionAnimation _selectionAnimation;
    
    public override void Select() => _selectionAnimation.Select();
    public override void Deselect() => _selectionAnimation.Deselect();
}
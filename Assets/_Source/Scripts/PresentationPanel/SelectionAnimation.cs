using UnityEngine;

public abstract class SelectionAnimation : MonoBehaviour
{
    public abstract void Select();
    public abstract void Deselect();
    public abstract void SelectInstantly();
    public abstract void DeselectInstantly();
}
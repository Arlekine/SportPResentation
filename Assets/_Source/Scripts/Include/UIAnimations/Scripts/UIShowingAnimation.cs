using DG.Tweening;
using UnityEngine;

public abstract class UIShowingAnimation : MonoBehaviour
{
    public abstract bool IsShowed { get; }
    
    public abstract Tween Show();
    public abstract Tween Hide();

    public abstract void ShowInstantly();
    public abstract void HideInstantly();

    public void Switch()
    {
        if (IsShowed)
            Hide();
        else
            Show();
    }
}
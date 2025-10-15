using System;
using UnityEngine;

public interface ISubchapterView
{
    event Action<ISubchapterView> Clicked;

    RectTransform RectTransform { get; }
    void Select();
    void Deselect();
}
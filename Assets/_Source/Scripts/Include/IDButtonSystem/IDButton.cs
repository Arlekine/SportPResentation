using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class IDButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    public abstract string ID { get; }

    public event Action<string> Clicked;

    public abstract void Select();
    public abstract void Deselect();

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick() => Clicked?.Invoke(ID);
}
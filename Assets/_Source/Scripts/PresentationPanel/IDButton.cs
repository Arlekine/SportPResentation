using System;
using UnityEngine;
using UnityEngine.UI;


public abstract class IDButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private SelectionAnimation _selectionAnimation;

    public abstract string ID { get; }

    public event Action<string> Clicked;

    public SelectionAnimation SelectionAnimation => _selectionAnimation;

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
using System;
using System.Collections.Generic;
using UnityEngine;

public class IdButtonsGroup : MonoBehaviour
{
    [SerializeField] private List<IDButton> _buttons;

    public event Action<string> Clicked;

    private void OnEnable()
    {
        _buttons.ForEach(x => x.Clicked += OnClick);
    }

    private void OnDisable()
    {
        _buttons.ForEach(x => x.Clicked -= OnClick);
    }

    private void OnClick(string id)
    {
        foreach (var idButton in _buttons)
        {
            if (idButton.ID == id)
                idButton.SelectionAnimation.Select();
            else
                idButton.SelectionAnimation.Deselect();
        }

        Clicked?.Invoke(id);
    }
}
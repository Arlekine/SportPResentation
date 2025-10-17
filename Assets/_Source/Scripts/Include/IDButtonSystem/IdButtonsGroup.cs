using System;
using System.Collections.Generic;
using UnityEngine;

public class IdButtonsGroup : MonoBehaviour
{
    [SerializeField] private List<IDButton> _buttons;

    public event Action<string> Clicked;

    protected virtual void OnEnable()
    {
        _buttons.ForEach(x => x.Clicked += OnClick);
    }

    protected virtual void OnDisable()
    {
        _buttons.ForEach(x => x.Clicked -= OnClick);
    }

    private void OnClick(string id)
    {
        foreach (var idButton in _buttons)
        {
            if (idButton.ID == id)
                idButton.Select();
            else
                idButton.Deselect();
        }

        Clicked?.Invoke(id);
    }
}
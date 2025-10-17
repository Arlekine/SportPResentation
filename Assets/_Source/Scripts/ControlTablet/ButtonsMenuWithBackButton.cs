using System;
using UnityEngine;
using UnityEngine.UI;

namespace View.ControlSensor
{
    public class ButtonsMenuWithBackButton : ButtonsMenu
    {
        [SerializeField] private Button _backButton;

        public event Action Back; 

        protected override void OnEnable()
        {
            base.OnEnable();
            _backButton.onClick.AddListener(OnBackClicked);
        }

        protected override void OnDisable()
        {
            base.OnEnable();
            _backButton.onClick.RemoveListener(OnBackClicked);
        }

        private void OnBackClicked() => Back?.Invoke();
    }
}
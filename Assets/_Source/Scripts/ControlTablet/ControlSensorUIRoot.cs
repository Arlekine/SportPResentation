using System;
using UnityEngine;

namespace View.ControlSensor
{
    public class ControlSensorUIRoot : MonoBehaviour
    {
        [SerializeField] private ButtonsMenu _regionsMenu;
        [SerializeField] private ButtonsMenuWithBackButton _chaptersMenu;

        private string _regions;

        public event Action<string, string> ChapterChanged;
        public event Action ReturnedToRegions;
    }
} 
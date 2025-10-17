using UnityEngine;

namespace View.ControlSensor
{
    public class ButtonsMenu : IdButtonsGroup
    {
        [SerializeField] private UIShowingAnimation _showingAnimation;

        public UIShowingAnimation ShowingAnimation => _showingAnimation;
    }

    public class ControlSensorChapterHeader : MonoBehaviour
    {
        [SerializeField] private UIShowingAnimation _showingAnimation;

        public UIShowingAnimation ShowingAnimation => _showingAnimation;
    }
}
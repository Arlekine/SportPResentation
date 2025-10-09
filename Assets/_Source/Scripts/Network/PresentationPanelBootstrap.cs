using Cysharp.Threading.Tasks;
using SquidGameVR.App;
using UnityEngine;

namespace SportPresentation.App
{
    public class PresentationPanelBootstrap : MonoBehaviour
    {
        private PresentationPanelSwitcher _presentationPanelSwitcher; 

        public void Init(ServiceLocator serviceLocator)
        {
            _presentationPanelSwitcher = new PresentationPanelSwitcher();
            _presentationPanelSwitcher.Init().Forget();
        }
    }
}
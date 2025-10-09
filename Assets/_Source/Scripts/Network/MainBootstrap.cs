using SquidGameVR.App;
using UnityEngine;

namespace SportPresentation.App
{
    public class MainBootstrap : MonoBehaviour
    {
        [SerializeField] private NetworkSlideSwitcher _slideSwitcher;
        [SerializeField] private ControlTabletBootstrap _controlTabletBootstrapPrefab;
        [SerializeField] private PresentationPanelBootstrap _presentationPanelBootstrapPrefab;
        
        private DisposablesHolder _disposablesHolder;

        public void Init(bool isServer)
        {
            var serviceLocator = new ServiceLocator();
            var contentLoader = new ContentLoader();
            _disposablesHolder = new DisposablesHolder();

            _disposablesHolder.Add(serviceLocator);

            serviceLocator.AddService<IDisposablesHolder>(_disposablesHolder);
            serviceLocator.AddService(contentLoader);

            if (isServer)
            {
                serviceLocator.AddService<ISlideSwitcher>(_slideSwitcher);
                var bootstrap = Instantiate(_presentationPanelBootstrapPrefab, transform);
                bootstrap.Init(serviceLocator);
            }
            else
            {
                serviceLocator.AddService<NetworkSlideSwitcher>(_slideSwitcher);
                var bootstrap = Instantiate(_controlTabletBootstrapPrefab, transform);
                bootstrap.Init(serviceLocator);
            }
        }

        private void OnDestroy()
        {
            _disposablesHolder.DisposeAll();
        }
    }
}
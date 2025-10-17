using SquidGameVR.App;
using UnityEngine;

namespace SportPresentation.App
{
    public class ControlTabletBootstrap : MonoBehaviour
    {
        private const string CONTROL_TABLET_UI_ROOT_PATH = "ControlTabletUIRoot";

        private IServiceLocator _serviceLocator;

        public async void Init(ServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;

            var prefab = await serviceLocator.GetService<ContentLoader>().LoadControlTabletContent<ControlTabletUIRoot>(CONTROL_TABLET_UI_ROOT_PATH);
            var menu = Instantiate(prefab);

            menu.ChapterChanged += OnChapterChanged;
        }

        private void OnChapterChanged(string regionID, string chapterID)
        {
            _serviceLocator.GetService<NetworkSlideSwitcher>().SwitchChapterServerRpc(regionID, chapterID);
        }
    }

    public class ControlSensorBootstrap : MonoBehaviour
    {
        private const string CONTROL_TABLET_UI_ROOT_PATH = "ControlTabletUIRoot";

        private IServiceLocator _serviceLocator;

        public async void Init(ServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;

            var prefab = await serviceLocator.GetService<ContentLoader>().LoadControlTabletContent<ControlTabletUIRoot>(CONTROL_TABLET_UI_ROOT_PATH);
            var menu = Instantiate(prefab);

            menu.ChapterChanged += OnChapterChanged;
        }

        private void OnChapterChanged(string regionID, string chapterID)
        {
            _serviceLocator.GetService<NetworkSlideSwitcher>().SwitchChapterServerRpc(regionID, chapterID);
        }
    }
}
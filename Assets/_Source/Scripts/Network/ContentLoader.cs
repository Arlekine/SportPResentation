using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SportPresentation.App
{
    public class ContentLoader
    {
        private const string PRESENTATION_PANEL_DATA_PATH = "Server";
        private const string CONTROL_TABLET_DATA_PATH = "Client";

        public async UniTask<T> LoadPresentationPanelContent<T>(string contentPath) where T : Object
        {
            var content = await Load<T>($"{PRESENTATION_PANEL_DATA_PATH}/{contentPath}");
            return content;
        }

        public async UniTask<T> LoadControlTabletContent<T>(string contentPath) where T : Object
        {
            var content = await Load<T>($"{CONTROL_TABLET_DATA_PATH}/{contentPath}");
            return content;
        }

        private async UniTask<T> Load<T>(string path) where T : Object
        {
            var content = await Resources.LoadAsync<T>(path).ToUniTask();
            return (T)content;
        }
    }
}
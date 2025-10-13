using System.Collections;
using NUnit.Framework;
using Unity.Netcode;
using UnityEngine;

namespace SportPresentation.App
{
    public class NetworkStart : MonoBehaviour
    {
        [SerializeField] private NetworkManager _networkManagerPrefab;
        [SerializeField] private NetworkConnector _networkConnector;
        [SerializeField] private MainBootstrap _mainBootstrap;
        [SerializeField] private bool _isServer;

        private IEnumerator Start()
        {
            var networkManager = Instantiate(_networkManagerPrefab);
            var networkState = new NetworkState(_isServer);
            var networkDiscovery = networkManager.GetComponent<ExampleNetworkDiscovery>();

            yield return null;

            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 120;

            _networkConnector.Init(networkState, networkDiscovery);
            _networkConnector.Connect();

            _mainBootstrap.Init(_isServer);
        }
    }
}
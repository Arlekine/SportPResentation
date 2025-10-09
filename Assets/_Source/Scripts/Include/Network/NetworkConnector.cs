using System;
using System.Collections;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkConnector : NetworkBehaviour
{
    private NetworkState _networkState;
    private ExampleNetworkDiscovery _serverIpBroadcaster;

    private bool _stopDiscovery;
    
    public void Init(NetworkState networkState, ExampleNetworkDiscovery serverIpBroadcaster)
    {
        _networkState = networkState;
        _serverIpBroadcaster = serverIpBroadcaster;
    }

    public void Connect()
    {
        NetworkManager.Singleton.OnServerStarted += () =>
        {
            Log.Info($"Server started on port: {_networkState.ServerPort}");
        };

        NetworkManager.Singleton.OnTransportFailure += () =>
        {
            Log.Info($"Transport failure");

            _networkState.ServerStarted = false;
            _networkState.ConnectedToServer = false;
        };

        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
            {
                Log.Info($"Client connected [{id}]");
            }
            else
            {
                Log.Info($"Connected to server, ID: {id}");

                _networkState.ConnectedToServer = true;
            }
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (_networkState.IsServer)
            {
                Log.Info($"Client disconnected [{id}]");
            }
            else
            {
                if (_networkState.ConnectedToServer == false)
                {
                    Log.Info($"Failed to connect to server");
                }
                else
                {
                    Log.Info($"Disconnected from server");
                    _networkState.ConnectedToServer = false;
                }

                _networkState.ServerIp = "";

                _serverIpBroadcaster.StartClient();
                _serverIpBroadcaster.OnServerFound.AddListener(OnServerFound);
                StartCoroutine(ServerIPFindingRoutine());
            }
        };

        if (_networkState.IsServer)
        {
            StartServer();
            _serverIpBroadcaster.StartServer();
        }
        else
        {
            if (string.IsNullOrEmpty(_networkState.ServerIp))
            {
                _serverIpBroadcaster.StartClient();
                _serverIpBroadcaster.OnServerFound.AddListener(OnServerFound);
                StartCoroutine(ServerIPFindingRoutine());
            }
            else 
            {
                StartClient();
            }
        }
    }

    private void Update()
    {
        if (_stopDiscovery)
        {
            _stopDiscovery = false;
            _serverIpBroadcaster.StopDiscovery();
        }
    }

    private void OnApplicationQuit()
    {
        ForceShutdown();
    }

    private void OnDestroy()
    {
        ForceShutdown();
    }

    private void ForceShutdown()
    {
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsListening)
        {
            Log.Info("Force shutdown server...");
            foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds)
            {
                NetworkManager.Singleton.DisconnectClient(clientId);
            }
            
            NetworkManager.Singleton.Shutdown();
        }

        if (_serverIpBroadcaster != null && _serverIpBroadcaster.IsRunning)
        {
            _serverIpBroadcaster.StopDiscovery();
        }

        StopAllCoroutines();
    }

    private IEnumerator ServerIPFindingRoutine()
    {
        while (_serverIpBroadcaster != null && _serverIpBroadcaster.IsRunning)
        {
            _serverIpBroadcaster.ClientBroadcast(new DiscoveryBroadcastData());
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnServerFound(IPEndPoint sender, DiscoveryResponseData response)
    {
        _serverIpBroadcaster.OnServerFound.RemoveListener(OnServerFound);
        
        StopAllCoroutines();

       _networkState.ServerIp = sender.Address.ToString();
       _networkState.ServerPort = response.Port;

       StartClient();

       _stopDiscovery = true;
    }

    [ContextMenu("StartServer")]
    public void StartServer()
    {
        InitUnityTransport();
        
        if (NetworkManager.Singleton.StartServer())
        {
            _serverIpBroadcaster.StartServer();
            _networkState.ServerStarted = true;
        }
    }
    
    [ContextMenu("StartClient")]
    public void StartClient()
    {
        InitUnityTransport();
        
        if (NetworkManager.Singleton.StartClient())
            Log.Info($"Connecting to server [{_networkState.ServerIp}:{_networkState.ServerPort}]...");
    }

    void InitUnityTransport()
    {
        UnityTransport transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.SetConnectionData(_networkState.ServerIp, _networkState.ServerPort);
    }
}

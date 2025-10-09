using System.Collections.Generic;
using System.Net;

public class NetworkState
{
	public bool IsServer = true;

    public List<string> ServerIpList = new List<string>();
    public int UdpBroadcastPortServer = 12345;
    public int UdpBroadcastPortClient = 54321;
    public string ServerIp;
    public ushort ServerPort = 6666;

    public bool ServerStarted;
    public bool ConnectedToServer;

    public NetworkState(bool isServer)
    {
        IsServer = isServer;
        if (IsServer)
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            string ips = "";
            bool first = true;
            string last = "";

            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                if (ipaddress.ToString().Length <= 15)
                {
                    if (first)
                    {
                        first = false;
                        ServerIp = ipaddress.ToString();
                    }
                    else
                    {
                        ips += ", ";
                    }

                    ips += ipaddress;

                    ServerIpList.Add(ipaddress.ToString());
                    last = ipaddress.ToString();
                }
            }

            ServerIp = last;

            Log.Info($"Server IP list: {ips}");
            Log.Info($"Server selected IP: {ServerIp}");
        }
    }
}

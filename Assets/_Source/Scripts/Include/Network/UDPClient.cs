using System.Net.Sockets;
using System.Text;

public class UDPClient
{
	public int Broadcast (int port, byte[] data)
	{
		return Send("255.255.255.255", port, data);
	}

	public int Broadcast (int port, string data)
	{
		return Send("255.255.255.255", port, Encoding.ASCII.GetBytes (data));
	}

	public int Send(string ip, int port, string data)
	{
		return Send(ip, port, Encoding.ASCII.GetBytes (data));
	}

	public int Send(string ip, int port, byte[] data)
	{
		int sent = 0;

		UdpClient udpClient = new UdpClient();
		
		try {
			sent = udpClient.Send (data, data.Length, ip, port);
		} catch (SocketException e) {
			Log.Error(e.Message);
		}

		return sent;
	}
}
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPServer
{
	public Action onStarted;
	public Action<byte[]> onRecv;
    public bool Started { get; private set; }

    private Thread _thread;
	private UdpClient _listener;

    private ThreadEventsDispatcher _threadEventsDispatcher;

    public UDPServer(ThreadEventsDispatcher threadEventsDispatcher)
    {
        _threadEventsDispatcher = threadEventsDispatcher;
    }

    public void Start (int port)
	{
		Log.Info("Starting...");

        Started = true;
		_thread = new Thread (ReceiveThread);
		_thread.IsBackground = true;
		_thread.Start (port);
	}

	public void Stop ()
	{
		Log.Info("Stopping...");

		if (Started) {
            Started = false;
			_listener.Close ();
			_thread.Abort ();
		}
	}

	void ReceiveThread (object port)
    {
        _threadEventsDispatcher.Enqueue(() => Log.Info("UDPServer started on port: " + (int)port));

        _listener = new UdpClient ();
		_listener.Client.Bind (new IPEndPoint (IPAddress.Any, (int)port));
		var from = new IPEndPoint (0, 0);

		byte[] data = null;

		onStarted?.Invoke();

		while (Started)
        {
            try
            {
				data = _listener.Receive (ref from);
			} 
            
            catch (Exception e) 
            {
				if (Started)
                    _threadEventsDispatcher.Enqueue(() => Log.Error(e.Message));
            }

            _threadEventsDispatcher.Enqueue(() => onRecv?.Invoke(data));
        }
	}
}
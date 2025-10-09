using UnityEngine;
using System.Collections;
using System.IO;

public class LogHandler : MonoBehaviour
{
#if !UNITY_ANDROID

	public string folder = "logs";
	public string prefix = "app";

	private string m_file;

	void Awake ()
	{
		string path = Application.dataPath + "/../" + folder;
		System.IO.Directory.CreateDirectory (path);
		m_file = path + "/" + prefix + "_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".log";
		Application.logMessageReceived += HandleLog;
	}

	void OnDestroy ()
	{
		Application.logMessageReceived -= HandleLog;
	}

	void HandleLog (string logString, string stackTrace, LogType type)
	{
		string pre = "[" + System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss.ms") + "] - [" + type.ToString() + "] - ";
		System.IO.File.AppendAllText (m_file, pre + logString + "\r\n");
		if (LogType.Exception == type) {
			System.IO.File.AppendAllText (m_file, stackTrace + "\r\n\r\n");
		}
	}
#endif
}

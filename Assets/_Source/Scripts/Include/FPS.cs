using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
	[SerializeField] private TMP_Text _text;
    [SerializeField] private float _updateInterval = 0.5F;

	private double _lastInterval;
	private int _frames = 0;
	private float _fps;

	void Start ()
	{
		_lastInterval = Time.realtimeSinceStartup;
		_frames = 0;
	}

	void Update ()
	{
			++_frames;
			float timeNow = Time.realtimeSinceStartup;
			if (timeNow > _lastInterval + _updateInterval) {
				_fps = _frames / (timeNow - (float)_lastInterval);
				_frames = 0;
				_lastInterval = timeNow;
			}

            _text.text = "FPS: " + ((int)_fps).ToString ();
	}
}

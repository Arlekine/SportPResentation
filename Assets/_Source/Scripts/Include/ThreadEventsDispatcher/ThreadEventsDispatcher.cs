using System;
using System.Collections.Concurrent;
using UnityEngine;

public class ThreadEventsDispatcher : MonoBehaviour
{
    private ConcurrentQueue<Action> _events;

    public ThreadEventsDispatcher()
    {
        _events = new ConcurrentQueue<Action>();
    }

    public void Enqueue(Action action)
    {
        _events.Enqueue(action);
    }

    private void Update()
    {
        Action action;

        while (_events.Count > 0 && _events.TryDequeue(out action))
        {
            action?.Invoke();
        }
    }
}
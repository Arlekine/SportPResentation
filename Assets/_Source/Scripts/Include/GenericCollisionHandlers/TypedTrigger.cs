using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class TypedTrigger<T> : MonoBehaviour
{
    public event Action<T> TriggerEnter;
    public event Action<T> TriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        var targetComponent = other.GetComponent<T>();

        if (targetComponent != null)
        {
            TriggerEnter?.Invoke(targetComponent);
            OnEnterTriggered(targetComponent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var targetComponent = other.GetComponent<T>();

        if (targetComponent != null)
        {
            TriggerExit?.Invoke(targetComponent);
            OnExitTriggered(targetComponent);
        }
    }

    protected virtual void OnEnterTriggered(T other)
    { }

    protected virtual void OnExitTriggered(T other)
    { }

    protected virtual void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }
}
using UnityEngine;
using UnityEngine.Events;


public class EventChannel<T> : ScriptableObject
{
    public event UnityAction<T> OnEvent;

    public void RaiseEvent(T value)
    {
        OnEvent.Invoke(value);
    }
}

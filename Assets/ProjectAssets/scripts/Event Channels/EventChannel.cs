using UnityEngine;
using UnityEngine.Events;


public class EventChannel<T> : ScriptableObject
{
    public event UnityAction<T> OnEvent;
    
    public void RaiseEvent(T value)
    {
        if(OnEvent == null)
        {
            Debug.LogWarning("No Listeners to event channel:" + name);
            return;
        }

        OnEvent.Invoke(value);
    }
}

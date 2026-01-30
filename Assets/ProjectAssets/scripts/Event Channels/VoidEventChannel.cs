using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "VoidEventChannel", menuName = "Scriptable Objects/Event Channels/Void")]
public class VoidEventChannel : ScriptableObject
{
    public event UnityAction OnEvent;

    public void RaiseEvent()
    {
        if (OnEvent == null)
        {
            Debug.LogWarning("No Listeners to event channel:" + name);
            return;
        }

        OnEvent.Invoke();
    }
}

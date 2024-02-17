using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class EventChannelSO<T> : ScriptableObject
{
    private UnityAction<T> _onEventInvoked;

    public void RegisterAction(UnityAction<T> a)
    {
        _onEventInvoked -= a;
        _onEventInvoked += a;
    }
    public void UnRegisterAction(UnityAction<T> a)
    {
        _onEventInvoked -= a;
    }
    public void Invoke(T arg)
    {
        _onEventInvoked?.Invoke(arg);
    }
}

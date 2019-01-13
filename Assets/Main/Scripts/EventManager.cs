using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RotaryHeart.Lib.SerializableDictionary;

[Serializable]
public class EventDictionaty : SerializableDictionaryBase<string, UnityEvent> { }

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private EventDictionaty eventDictionary;
    
    public void Subscribe(string eventName, UnityAction listener)
    {
        UnityEvent currentEvent = null;
        if (eventDictionary.TryGetValue(eventName, out currentEvent))
        {
            currentEvent.AddListener(listener);
        }
        else
        {
            currentEvent = new UnityEvent();
            currentEvent.AddListener(listener);
            eventDictionary.Add(eventName, currentEvent);
        }
    }

    public void Unsubscribe(string eventName, UnityAction listener)
    {
        UnityEvent currentEvent = null;
        if (eventDictionary.TryGetValue(eventName, out currentEvent))
        {
            currentEvent.RemoveListener(listener);
        }
    }

    public void Fire(string eventName)
    {
        UnityEvent currentEvent = null;
        if (eventDictionary.TryGetValue(eventName, out currentEvent))
        {
            currentEvent.Invoke();
        }
    }
}

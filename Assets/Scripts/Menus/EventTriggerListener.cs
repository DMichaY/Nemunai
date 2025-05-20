using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class EventTriggerListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action onEnter;
    public Action onExit;

    public static EventTriggerListener Get(GameObject obj)
    {
        EventTriggerListener listener = obj.GetComponent<EventTriggerListener>();
        if (listener == null)
            listener = obj.AddComponent<EventTriggerListener>();
        return listener;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onExit?.Invoke();
    }
}

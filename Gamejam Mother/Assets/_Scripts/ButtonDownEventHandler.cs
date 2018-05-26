using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonDownEventHandler : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent ButtonDownEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonDownEvent.Invoke();
    }
}

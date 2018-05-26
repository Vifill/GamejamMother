using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[AddComponentMenu("Event/DoubleClickEvent")]
public class DoubleClickEventHandler : MonoBehaviour, IPointerClickHandler 
{
    public UnityEvent DoubleClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            DoubleClickEvent.Invoke();
            Debug.Log("Double Clicked");
        }
    }
}

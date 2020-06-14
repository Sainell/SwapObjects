using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectClick : MonoBehaviour, IPointerClickHandler
{
    public static event Action<Transform> clickEvent;

    public Transform CallObjectTag ()
    {
        var transform = gameObject.transform;
        return transform;
    }

    public void OnPointerClick(PointerEventData eventData)
    {      
        clickEvent?.Invoke(CallObjectTag());
    }
}

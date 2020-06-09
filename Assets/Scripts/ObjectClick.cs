using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectClick : MonoBehaviour, IPointerClickHandler
{
    public static event Action<string> clickEvent;

    public string CallObjectTag ()
    {
        var tag = gameObject.tag;
        Debug.Log($"My name is {tag}");
        return tag;
    }

    public void OnPointerClick(PointerEventData eventData)
    {      
        clickEvent?.Invoke(CallObjectTag());
    }
}

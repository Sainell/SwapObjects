using UnityEngine;
using UnityEngine.EventSystems;


public class MenuExit : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}

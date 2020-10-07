using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPlay : MonoBehaviour, IPointerClickHandler
{
    public GameObject GameInterface;
    public GameObject MenuInterface;


    public void OnPointerClick(PointerEventData eventData)
    {
        GameInterface.SetActive(true);
        MenuInterface.SetActive(false);
    }

    private void Awake()
    {
        MenuInterface = transform.parent.gameObject;
        GameInterface = GameObject.Find("Background").gameObject;
    }
    private void Start()
    {

        GameInterface.SetActive(false);
    }
}

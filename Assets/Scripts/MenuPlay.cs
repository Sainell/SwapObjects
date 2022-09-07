using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPlay : MonoBehaviour, IPointerClickHandler
{
    public GameObject GameInterface;
    public GameObject MenuInterface;
    public GameObject SelectionInterface;


    public void OnPointerClick(PointerEventData eventData)
    {
      //  GameInterface.SetActive(true);
        MenuInterface.SetActive(false);
        SelectionInterface.SetActive(true);
    }

    private void Awake()
    {
        MenuInterface = transform.parent.gameObject;
        GameInterface = GameObject.Find("Background").gameObject;
        SelectionInterface = GameObject.Find("").gameObject;
    }
    private void Start()
    {
        Debug.Log("qweq");
        GameInterface.SetActive(false);
        SelectionInterface.SetActive(false);
    }
}

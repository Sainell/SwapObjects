using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectGenerator : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>();
    private List<GameObject> tempObjectList = new List<GameObject>();
    private List<GameObject> usedObjectList = new List<GameObject>();
    private int num;
    private Image img;

    public GameObject tempCenterObject;
    public GameObject tempLeftUpObject;
    public GameObject tempLeftDownObject;
    public GameObject tempRightUpObject;
    public GameObject tempRightDownObject;

    public GameObject position0;
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject position4;

    private void Awake()
    {
        ObjectClick.clickEvent += CheckObjects;
    }
    void Start()
    {
        GenerateCornerObjects();
        GenerateCenterObject();
    }

    public void GenerateCornerObjects()
    {
        tempObjectList.AddRange(objectList);

        CreateObject(ref tempLeftUpObject, position1.transform);
        CreateObject(ref tempLeftDownObject, position2.transform);
        CreateObject(ref tempRightUpObject, position3.transform);
        CreateObject(ref tempRightDownObject, position4.transform);
    }

    public void CreateObject(ref GameObject tempObject, Transform objTransform)
    {
        var objNum = Random.Range(0, tempObjectList.Count);

        tempObject = Instantiate(tempObjectList[objNum], objTransform.position, objTransform.rotation, objTransform);
        tempObjectList.RemoveAt(objNum);
        usedObjectList.Add(tempObject);
    }

    public void GenerateCenterObject()
    {
        num = Random.Range(0, usedObjectList.Count);
        tempCenterObject = Instantiate(usedObjectList[num], position0.transform.position, position0.transform.rotation, position0.transform);
        img = tempCenterObject.GetComponent<Image>();
        img.color = new Color(0, 0, 0);
        img.raycastTarget = false;
    }
 
    public void CheckObjects(string tag)
    {
        if (tag.Equals(usedObjectList[num].tag))
        {
            Destroy(tempCenterObject);
            Destroy(tempLeftUpObject);
            Destroy(tempLeftDownObject);
            Destroy(tempRightDownObject);
            Destroy(tempRightUpObject);

            usedObjectList.Clear();
            tempObjectList.Clear();

            GenerateCornerObjects();
            GenerateCenterObject();
        }
        else
        {
            Debug.Log("Choose other object");
        }
    }
}

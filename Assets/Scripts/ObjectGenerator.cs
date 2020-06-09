using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>();
    public int num;
    public GameObject tempObject;

    private void Awake()
    {
        ObjectClick.clickEvent += CheckObjects;
    }
    void Start()
    {
        GenerateObject();
    }

    public void GenerateObject()
    {
        num = Random.Range(0, 3);
      tempObject =  Instantiate(objectList[num], transform.position, transform.rotation, transform);
    }

    public void CheckObjects(string tag)
    {
        if (tag.Equals(objectList[num].tag))
        {
            Destroy(tempObject);
            GenerateObject();
        }
        else
        {
            Debug.Log("Choose other object");
        }
    }
}

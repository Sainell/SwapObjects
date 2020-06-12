using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectGenerator : MonoBehaviour
{
    private List<Sprite> _tempImageList = new List<Sprite>();
    private List<Sprite> _usedImageList = new List<Sprite>();
    private List<GameObject> _usedObjectList;
    private int _num;
    private Text _score;
    private int _scoreCount = 0;
    private Image img;
    private GameObject tempCenterObject;

    public List<Sprite> imageList = new List<Sprite>();

    public GameObject position0;
    public GameObject position1;
    public GameObject position2;
    public GameObject position3;
    public GameObject position4;

    

    public Image img0;
    public Image img1;
    public Image img2;
    public Image img3;
    public Image img4;

    private void Awake()
    {
        _score = GameObject.Find("score").GetComponent<Text>();
        ObjectClick.clickEvent += CheckObjects;
        for (int i = 1; i <= 41; i++)
        {
            imageList.Add(Resources.Load<Sprite>($"Images/Animals/{i}"));
        }

        img1 = position1.GetComponent<Image>();
        img2 = position2.GetComponent<Image>();
        img3 = position3.GetComponent<Image>();
        img4 = position4.GetComponent<Image>();

        _usedObjectList = new List<GameObject>{position1, position2,position3,position4};
    }
    void Start()
    {
        GenerateCornerObjects();
        GenerateCenterObject();
    }

    public void GenerateCornerObjects()
    {
        _tempImageList.AddRange(imageList);
        CreateObject(ref img1);
        CreateObject(ref img2);
        CreateObject(ref img3);
        CreateObject(ref img4);
    }

    public void CreateObject(ref Image img)
    {
        var imgNum = Random.Range(0, _tempImageList.Count);
        img.sprite = _tempImageList[imgNum];
        _usedImageList.Add(_tempImageList[imgNum]);
        _tempImageList.RemoveAt(imgNum);
        
    }

    public void GenerateCenterObject()
    {
        _num = Random.Range(0, _usedObjectList.Count);
     //   img0.sprite = _usedImageList[_num];
        tempCenterObject = Instantiate(_usedObjectList[_num], position0.transform.position, position0.transform.rotation, position0.transform);
        img = tempCenterObject.GetComponent<Image>();
        img.color = new Color(0, 0, 0);
        img.raycastTarget = false;

    }
 
    public void CheckObjects(string tag)
    {
        if (tag.Equals(tempCenterObject.tag))
        {
            _scoreCount++;
            Destroy(tempCenterObject);

            _usedImageList.Clear();
            _tempImageList.Clear();

            GenerateCornerObjects();
            GenerateCenterObject();
        }
        else
        {
            _scoreCount = 0;
            Debug.Log("Choose other object");
        }

        _score.text = _scoreCount.ToString();

    }
}

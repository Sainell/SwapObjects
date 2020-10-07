using System.Collections;
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
    private AudioSource _mainSound;
    private AudioSource _fxSound;
    private Image[] _soundSwitcher;
    private AudioClip[] _fxClips = new AudioClip[3];
    private ParticleSystem[] _fxStars = new ParticleSystem[4];
    private ParticleSystem[] _fxFinish = new ParticleSystem[32];
    private Animator[] _animators = new Animator[4];
    private WaitForSeconds waitTime = new WaitForSeconds(1f);
    private WaitForSeconds waitTimeFinish = new WaitForSeconds(7f);
    private bool canClick = true;
    private bool isFinish = false;
    private BubbleSpawner _bubbleSpawner;

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
        for(int j=1;j<=4;j++)
        {
            _fxStars[j-1]= GameObject.Find($"FxStars{j}").GetComponent<ParticleSystem>();
            _animators[j-1] = GameObject.Find($"Position{j}").GetComponent<Animator>();
        }
        _fxFinish = GameObject.Find("FxFinish").GetComponentsInChildren<ParticleSystem>();
        _score = GameObject.Find("score").GetComponent<Text>();
        _mainSound = GameObject.Find("MainSound").GetComponent<AudioSource>();
        _fxSound = GameObject.Find("FXSounds").GetComponent<AudioSource>();
        _soundSwitcher = GameObject.Find("SoundSwitcher").GetComponentsInChildren<Image>();
        _fxClips[0] = Resources.Load<AudioClip>($"Sounds/right");
        _fxClips[1] = Resources.Load<AudioClip>($"Sounds/fail");
        _fxClips[2] = Resources.Load<AudioClip>($"Sounds/ura");
        ObjectClick.clickEvent += CheckObjects;
        for (int i = 1; i <= 40; i++)
        {
            imageList.Add(Resources.Load<Sprite>($"Images/Animals/{i}"));
        }

        img1 = position1.GetComponent<Image>();
        img2 = position2.GetComponent<Image>();
        img3 = position3.GetComponent<Image>();
        img4 = position4.GetComponent<Image>();

        _usedObjectList = new List<GameObject>{position1, position2,position3,position4};
        _bubbleSpawner = GameObject.Find("BubbleSpawner").GetComponent<BubbleSpawner>();
    }
    void Start()
    {
        GenerateCornerObjects();
        GenerateCenterObject();
        _mainSound.Play();
    }

    private void Update()
    {

#if UNITY_ANDROID
        if (canClick)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        CheckObjects(hit.transform);
                    }
                }
            }
        }
#endif
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
        tempCenterObject = Instantiate(_usedObjectList[_num], position0.transform.position, position0.transform.rotation, position0.transform);
        img = tempCenterObject.GetComponent<Image>();
        img.color = new Color(0, 0, 0);
        img.raycastTarget = false;

    }

    public void SoundSwitcher()
    {
        if (_soundSwitcher[0].enabled)
        {
            SoundOff();
        }
        else
        {
            SoundOn();
        }
    }

    public void SoundOn()
    {
        _soundSwitcher[0].enabled = true;
        _soundSwitcher[1].enabled = false;
        _mainSound.Play();
        _fxSound.enabled = true;
    }

    public void SoundOff()
    {
        _soundSwitcher[0].enabled = false;
        _soundSwitcher[1].enabled = true;
        _mainSound.Stop();
        _fxSound.enabled = false;
    }

    IEnumerator Delay(WaitForSeconds delayTime)
    {
        yield return delayTime;

        Destroy(tempCenterObject);
        GenerateCornerObjects();
        GenerateCenterObject();
        _fxSound.Stop();
        if (isFinish)
        {
            _fxSound.PlayOneShot(_fxClips[0]);
            isFinish = false;
        }
        canClick = true;
    }

    public void CheckObjects(Transform transform)
    {
        if (canClick)
        {
            if (transform.tag.Equals("Exit"))
            {
                Application.Quit();
            }

            if (transform.tag.Equals("SoundSwitcher"))
            {
                SoundSwitcher();
            }

            if (transform.tag.Equals(tempCenterObject.tag))
            {
                foreach (var obj in _fxStars)
                {
                    if (obj.transform.tag.Equals(tempCenterObject.tag))
                    {
                        obj.time = 0.6f;
                        obj.Stop();
                        obj.Play();
                        break;
                    }
                }
                canClick = false;
                _scoreCount++;
                var delayTime = waitTime;
                if(_scoreCount%10==0)
                {
                    isFinish = true;
                    delayTime = waitTimeFinish;
                    _fxSound.PlayOneShot(_fxClips[2]);
               //     _bubbleSpawner.StartSpawn(500);
                    for (int i=0;i<_fxFinish.Length;i++)
                    {
                        _fxFinish[i].Play();
                    }
                }
                _fxSound.PlayOneShot(_fxClips[0]);
               
                _usedImageList.Clear();
                _tempImageList.Clear();
                
                StartCoroutine(Delay(delayTime));
            }
            else if (!transform.tag.Equals("Exit") & !transform.tag.Equals("SoundSwitcher"))
            {
                _fxSound.PlayOneShot(_fxClips[1]);
                _scoreCount = 0;
                foreach (var obj in _animators)
                {
                    if (obj.transform.tag.Equals(transform.tag))
                    {
                        obj.Play("shake");
                        break;
                    }
                }
            }
            _score.text = _scoreCount.ToString();
        }
    }
}

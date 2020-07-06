using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Bubble : MonoBehaviour , IPointerClickHandler
{
    Animator animator;
    public bool readyToMove;
    public bool isLeft;
    public int speed;
    public AudioSource bubbleSource;
    public AudioClip bubbleClip;

    public void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        readyToMove = true;
        Destroy(gameObject, 5);
        bubbleClip = Resources.Load<AudioClip>($"Sounds/bubble");
        bubbleSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        MoveUp();


        if (isLeft)
        {
            MoveLeft();
        }
        else
        {
            MoveRight();
        }

    }

    public void MoveUp()
    {
        gameObject.transform.Translate(gameObject.transform.up * Time.deltaTime * speed);
       
    }

    public void MoveRight()
    {
        gameObject.transform.Translate(gameObject.transform.right * Time.deltaTime * 0.5f);
        if (readyToMove)
        {
            readyToMove = false;
            StartCoroutine(Delay(new WaitForSeconds(0.2f)));
        }
    }
    public void MoveLeft()
    {
        gameObject.transform.Translate(gameObject.transform.right *-1 * Time.deltaTime * 0.5f);
        if (readyToMove)
        {
            readyToMove = false;
            StartCoroutine(Delay(new WaitForSeconds(0.2f)));
        }
    }

    IEnumerator Delay(WaitForSeconds seconds)
    {       
        yield return seconds;
        if (isLeft)
        {
            isLeft = false;
        }
        else
        {
            isLeft = true;
        }
        readyToMove = true;
    }

    public void BubbleKill()
    {
        bubbleSource.PlayOneShot(bubbleClip);
        animator.Play("BubbleAnimation");
        Destroy(gameObject, 0.15f);
      
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BubbleKill();
    }
}

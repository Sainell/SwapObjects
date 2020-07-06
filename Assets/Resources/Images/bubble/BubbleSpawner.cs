using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject[] bubbleList = new GameObject[6];

    public void Spawn()
    {
        var num = Random.Range(0, 4);
        var vectorX = Random.Range(-10, 10);
        var vectorY = Random.Range(-20, -5);
        Instantiate(bubbleList[num], new Vector3(vectorX, vectorY), new Quaternion(), gameObject.transform);

    }
    public void StartSpawn(int limit)
    {
        for (int i = 0; i < limit; i++)
        {
            var waintTime = Random.Range(0, 1.5f);
            StartCoroutine(Delay(new WaitForSeconds(waintTime)));
        }
    }
    IEnumerator Delay(WaitForSeconds seconds)
    {
        yield return seconds;
        Spawn();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRotation : MonoBehaviour
{
    public float RotateSpeed = 300f;

    private void RotateObj()
    {
        transform.Rotate(0, 0 , RotateSpeed * Time.deltaTime);
    }
    private void Update()
    {
        RotateObj();
    }
}

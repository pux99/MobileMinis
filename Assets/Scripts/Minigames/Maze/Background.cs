using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private void Awake()
    {
        Camera _cam = Camera.main;
        
        float screenWidthWorld = _cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, _cam.nearClipPlane)).x - _cam.ScreenToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane)).x;
        float screenHeightWorld = _cam.ScreenToWorldPoint(new Vector3(0, Screen.height, _cam.nearClipPlane)).y - _cam.ScreenToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane)).y;
        
        transform.localScale = new Vector3(screenWidthWorld, screenHeightWorld, transform.localScale.z);
    }
}

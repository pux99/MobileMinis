using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = Camera.main;
        
        float screenHeight = cam.orthographicSize * 2;
        float screenWidth = screenHeight * Screen.width / Screen.height;
        
        transform.localScale = new Vector3(screenWidth, screenHeight, transform.localScale.z);
    }
}

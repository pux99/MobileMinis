using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    private Vector2 _startingPosition;
    private Vector2 endingPosition;
    [SerializeField]private int minimumDistance;

    public Action SwipeToTheRight;
    public Action SwipeToTheLeft;
    
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _startingPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endingPosition = Input.GetTouch(0).position;
            float diference = _startingPosition.x - endingPosition.x;
            if (Mathf.Abs(diference) > minimumDistance)
            {
                if (diference > 0)
                {
                    SwipeToTheLeft?.Invoke();
                    Debug.Log("left");
                }
                else
                {
                    SwipeToTheRight?.Invoke();
                    Debug.Log("right");
                }
            }

        }
    }

    public void SwipeToTheLeft1()
    {
        SwipeToTheLeft?.Invoke();
    }
    public void SwipeToTheRigth1()
    {
        SwipeToTheRight?.Invoke();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        GetComponent<SpriteRenderer>().color = other.gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
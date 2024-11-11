using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MazeManager.Instance.EndGame(true);
        }
        else if (other.CompareTag("Enemy"))
        {
            MazeManager.Instance.EndGame(false);
        }
    }
}

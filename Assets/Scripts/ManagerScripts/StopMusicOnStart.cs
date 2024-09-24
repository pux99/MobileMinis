using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusicOnStart : MonoBehaviour
{
    [SerializeField] AudioList audioList;
    [SerializeField] int songOnArray;
    [SerializeField] int soundOnArray;
    private void Start()
    {
        audioList.StopLoopingMusic(songOnArray);
        audioList.PlaySound(songOnArray);
    }
}

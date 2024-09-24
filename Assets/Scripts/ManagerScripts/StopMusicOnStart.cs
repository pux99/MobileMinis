using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusicOnStart : MonoBehaviour
{
    [SerializeField] AudioList audioList;
    [SerializeField] int songOnArray;
    [SerializeField] int soundOnArray;
    private void OnEnable()
    {
        audioList.StopLoopingMusic(songOnArray);
        audioList.PlaySound(songOnArray);
    }
}

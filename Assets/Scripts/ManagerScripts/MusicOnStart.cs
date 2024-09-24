using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnStart : MonoBehaviour
{
    [SerializeField] AudioList audioList;
    [SerializeField] int songOnArray;
    void Start()
    {
        audioList.PlayLoopingMusic(songOnArray);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnStart : MonoBehaviour
{
    [SerializeField] AudioList audioList;
    void Start()
    {
        audioList.PlayMusic(0);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using ManagerScripts;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialButtonButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorialOverlay;
    public void ButtonPressAction()
    {
        if(tutorialOverlay.activeInHierarchy)
            ServiceLocator.Instance.GetService<EventManager>().OnResume();
        else
        {
            ServiceLocator.Instance.GetService<EventManager>().OnPause();
        }
    }
}

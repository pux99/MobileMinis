using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ganar : MonoBehaviour
{
    Slider slider;
    public GameObject ganaste;
    public GameObject timer;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value<=0)
        {
            ganaste.SetActive(true);
            timer.SetActive(false);
        }
    }
}

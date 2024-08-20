using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class countDown : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;
    public UnityEngine.UI.Image shield;
    public TextMeshProUGUI timer;
    public float _timer = 30;
    public GameObject perdiste;
    public Animator animator;
    public void ResetTimer(int time)
    {
        _timer = time;
    }
    public void addTime(int time)
    {
        _timer += time;
    }
    public void removeTime(int time)
    {
        _timer -= time;
    }
    void Update()
    {
        _timer-=Time.deltaTime;
        timer.text=Mathf.CeilToInt(_timer).ToString();
        if(_timer < 0) 
        {
            _timer = 15;
            CountdownEndEvent();
        }
    }
    public void CountdownEndEvent()
    {
        if (shield.enabled)
        {
            shield.enabled = false;
            animator.SetTrigger("ataque");
        }
        else
        {
            slider.value -= 0.25f;
            animator.SetTrigger("ataque");
        }
        if(slider.value <= 0)
        {
            LossEvent();
        }
    }
    public void LossEvent()
    {
        perdiste.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

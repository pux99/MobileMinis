using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SimonButton : MonoBehaviour
{
    [SerializeField] public Button button;
    [SerializeField] public Color highLightColor;
    [SerializeField] public Color normalLightColor;
    [SerializeField] public Color disabledColor;

    private void Start()
    {
        BackToNormal();
    }

    public void Highlight()
    {
        var block = button.colors;
        block.disabledColor = highLightColor;
        button.colors = block;
    }
    public void BackToNormal()
    {
        var block = button.colors;
        block.normalColor = normalLightColor;
        block.pressedColor = highLightColor;
        block.disabledColor = disabledColor;
        button.colors = block;
    }
}

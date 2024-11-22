using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingButton : MonoBehaviour
{
    public float blinkSpeed = 1.0f;
    private readonly float _minAlpha = 0.5f;
    private readonly float _maxAlpha = 1.0f;
    private Image _image;
    private TextMeshProUGUI _text; 
    void Start()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * blinkSpeed, _maxAlpha - _minAlpha) + _minAlpha;
        Color imageColor = _image.color;
        imageColor.a = alpha;
        _image.color = imageColor;

        
        Color textColor = _text.color;
        textColor.a = alpha;
        _text.color = textColor;
    } 
}

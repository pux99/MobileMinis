using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedImage : Image
{
    [SerializeField] private float hitTestMinimumThreshold = .5f;
    [SerializeField] private float a ;
    protected override void OnValidate()
    {
        base.OnValidate();
        alphaHitTestMinimumThreshold = hitTestMinimumThreshold;
    }

    protected override void Awake()
    {
        base.Awake();
        alphaHitTestMinimumThreshold = hitTestMinimumThreshold;
    }
}

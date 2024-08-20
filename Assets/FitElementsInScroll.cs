using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FitElementsInScroll : MonoBehaviour
{
    List<RectTransform> elements = new List<RectTransform>();
    void Start()
    {
        foreach (Transform child in transform)
        {
            elements.Add(child.gameObject.GetComponent<RectTransform>());
        }
        var distanse= 1f/elements.Count;
        for (int i = 0; i < elements.Count; i++)
        {
            var element = elements[i];
            element.anchorMax = new Vector2(1, 1-distanse*i);
        }
        GetComponent<AspectRatioFitter>().aspectRatio =1f/(elements.Count);
    }

    public void ActivateButtons(GameObject ignore)
    {
        for (int i = 0;i < elements.Count;i++)
        {
            var element = elements[i];
            if (element.gameObject != ignore)
            {
                element.gameObject.GetComponent<Button>().interactable = true;
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    private Image tileImage;
    private Color finalColor;
    private bool isOccupied;

    private void Start()
    {
        tileImage = GetComponent<Image>();
        isOccupied = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isOccupied){tileImage.color = Color.gray;}
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isOccupied) {tileImage.color = Color.white; }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Image draggedInfo = DragManager.Instance.DraggedImage;
        isOccupied = true;    
        tileImage.sprite = draggedInfo.sprite;
        tileImage.color = draggedInfo.color;
    }
}

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

    [SerializeField] public Vector2Int gridPosition;

    private void Start()
    {
        tileImage = GetComponent<Image>();
        isOccupied = false;
    }
    
    public bool IsOccupied()
    {
        return isOccupied;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isOccupied || DragManager.Instance.OccupiedCells == null) return;
        DragManager.Instance.ColorNeighboringTiles(gridPosition, Color.gray);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isOccupied || DragManager.Instance.OccupiedCells == null) return;
        DragManager.Instance.ColorNeighboringTiles(gridPosition, Color.white);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isOccupied) return;
        DragManager.Instance.ColorNeighboringTiles(gridPosition, Color.white);
        DragManager.Instance.PlacePiece(gridPosition);
    }

    public void SetOccupied()
    {
        isOccupied = true;
        tileImage.color = DragManager.Instance.DraggedImage.color;
    }
    
    public void SetColor(Color color)
    {
        if (!isOccupied)
        {
            tileImage.color = color; 
        }
    }
}

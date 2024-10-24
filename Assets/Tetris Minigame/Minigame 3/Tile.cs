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
    private bool isHover;
    private bool isOccupied;

    [SerializeField] public Vector2Int gridPosition;

    private void Start()
    {
        tileImage = GetComponent<Image>();
        isOccupied = false;
        isHover = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tileImage.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tileImage.color = Color.white;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Vector2Int[] occupiedCells = DragManager.Instance.OccupiedCells;

        ActivateNeighboringTiles(occupiedCells);
    }

    private void SetTileOccupied()
    {
        isOccupied = true;
        tileImage.sprite = DragManager.Instance.DraggedImage.sprite;
        tileImage.color = DragManager.Instance.DraggedImage.color;
    }

    private void ActivateNeighboringTiles(Vector2Int[] occupiedCells)
    {
        foreach (Vector2Int cell in occupiedCells)
        {
            var targetPosition = gridPosition + cell;

            var targetTile = GridManager_MG3.Instance.GetTileAtPosition(targetPosition);
            if (targetTile != null)
            {
                targetTile.SetTileOccupied();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance { get; private set; }

    public Image DraggedImage { get; private set; }
    public Vector2 Size { get; private set; }
    public Vector2Int[] OccupiedCells { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetPieceInfo(Image image, Vector2 size, Vector2Int[] occupiedCells)
    {
        DraggedImage = image;
        Size = size;
        OccupiedCells = occupiedCells;
    }
    
    public void ClearPieceInfo()
    {
        DraggedImage = null;
        Size = Vector2.zero;
        OccupiedCells = null;
    }
}

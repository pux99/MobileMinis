using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChocolateFactory : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject moldPrefab;
    public GameObject chocolatePiecePrefab;
    
    public Transform parentContainer;

    private void Start()
    {
        MakeChocolateBar(6,8, Color.white);
        
        // En Rows minimo 2.
        // No mas de 20
    }

    public void MakeChocolateBar(int columns, int rows, Color color)
    {
        GameObject mold = Instantiate(moldPrefab, parentContainer);
        GridLayoutGroup grid = mold.GetComponent<GridLayoutGroup>();
        
        RectTransform moldRect = mold.GetComponent<RectTransform>();
        
        Vector2 moldSize = moldRect.rect.size;

        float cellWidth = moldSize.x / columns;
        float cellHeight = moldSize.y / rows;

        grid.cellSize = new Vector2(cellWidth, cellHeight);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = columns;
        
        grid.spacing = Vector2.zero;
        
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                GameObject chocolatePiece = Instantiate(chocolatePiecePrefab, mold.transform);
                
                Image pieceImage = chocolatePiece.GetComponent<Image>();
                pieceImage.color = color;
            }
        }
        mold.name = $"ChocolateBar_{columns}x{rows}";
    }
}

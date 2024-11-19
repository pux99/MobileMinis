using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChocolateFactory : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject moldPrefab;
    public GameObject chocolatePiecePrefab;
    
    public Transform parentContainer;

    private HashSet<string> generatedPairs = new HashSet<string>();
    
    public List<Chocolate> GenerateRandomChocolate(int x)
    {
        List<Chocolate> generatedChocolateBars = new List<Chocolate>();
        generatedPairs.Clear(); // Reset previously generated pairs

        System.Random random = new System.Random();
        int attempts = 0;

        while (generatedChocolateBars.Count < x && attempts < 100)
        {
            int columns = random.Next(1, 7); // Generate between 1 and 6
            int rows = random.Next(1, 7);

            if (columns * rows <= 20)
            {
                // Create a unique key for the pair
                string pairKey = $"{Mathf.Min(columns, rows)}-{Mathf.Max(columns, rows)}";

                if (!generatedPairs.Contains(pairKey))
                {
                    generatedPairs.Add(pairKey);

                    // Create the chocolate bar and add its Chocolate component to the list
                    GameObject chocolateBar = MakeChocolateBar(columns, rows, Color.blue);
                    Chocolate chocolateComponent = chocolateBar.GetComponent<Chocolate>();
                    chocolateComponent.Initialize(columns, rows, Color.blue);

                    generatedChocolateBars.Add(chocolateComponent);
                }
            }
            attempts++;
        }
        
        return generatedChocolateBars;
    }

    private GameObject MakeChocolateBar(int columns, int rows, Color color)
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

        return mold;
    }
}

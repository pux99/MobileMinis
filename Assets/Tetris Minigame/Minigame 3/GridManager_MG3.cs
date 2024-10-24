using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GridManager_MG3 : MonoBehaviour
{
    public static GridManager_MG3 Instance { get; private set; }
    
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;
    [SerializeField] private Tile _tile;
    
    private Tile[,] grid;

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
    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tile[gridWidth, gridHeight];

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                var tileObj = Instantiate(_tile, transform);
                var tile = tileObj.GetComponent<Tile>();
                tile.gridPosition = new Vector2Int(x, y);
                grid[x, y] = tile;
            }
        }
    }
    
    public Tile GetTileAtPosition(Vector2Int position)
    {
        if (position.x >= 0 && position.x < gridWidth && position.y >= 0 && position.y < gridHeight)
        
            return grid[position.x, position.y];
        
        return null;
    }
}

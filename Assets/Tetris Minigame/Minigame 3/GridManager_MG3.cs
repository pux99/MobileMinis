using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GridManager_MG3 : MonoBehaviour
{
    public static GridManager_MG3 Instance { get; private set; }
    
    private int _gridWidth;
    private int _gridHeight;
    [SerializeField] private Tile tile;
    
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
        grid = new Tile[_gridWidth, _gridHeight];

        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                var tileObj = Instantiate(tile, transform);
                var newTile = tileObj.GetComponent<Tile>();
                newTile.gridPosition = new Vector2Int(x, y);
                grid[x, y] = newTile;
            }
        }
    }
    
    public Tile GetTileAtPosition(Vector2Int position)
    {
        if (position.x >= 0 && position.x < _gridWidth && position.y >= 0 && position.y < _gridHeight)
        
            return grid[position.x, position.y];
        
        return null;
    }
}

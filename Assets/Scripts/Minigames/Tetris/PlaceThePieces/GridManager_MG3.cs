using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GridManager_MG3 : MonoBehaviour
{
    public static GridManager_MG3 Instance { get; private set; }
    
    public int gridWidth;
    public int gridHeight;
    [SerializeField] private Tile tile;
    
    private Tile[,] grid;

    private GridLayoutGroup _gridLayout;
    private RectTransform _transform;
    private Vector2  _tileSize = new Vector2(235,235);
    private float maxWidth = 1350;
    private float maxHeight = 1050;

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
        GenerateGridLayout();
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tile[gridWidth, gridHeight];

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                var tileObj = Instantiate(tile, transform);
                var newTile = tileObj.GetComponent<Tile>();
                newTile.gridPosition = new Vector2Int(x, y);
                grid[x, y] = newTile;
            }
        }
    }

    void GenerateGridLayout()
    {
        _transform = GetComponent<RectTransform>();
        _gridLayout = GetComponent<GridLayoutGroup>();
        
        float totalWidth = gridWidth * _tileSize.x;
        float totalHeight = gridHeight * _tileSize.y;
        
        if (totalWidth > maxWidth || totalHeight > maxHeight)
        {
            float widthScaleFactor = maxWidth / totalWidth;
            float heightScaleFactor = maxHeight / totalHeight;
            float scaleFactor = Mathf.Min(widthScaleFactor, heightScaleFactor);
            
            _tileSize *= scaleFactor;
            _gridLayout.cellSize = _tileSize;
        }
        
        _transform.sizeDelta = new Vector2(_tileSize.x * gridWidth, _tileSize.y * gridHeight); 
        
    }
    public Tile GetTileAtPosition(Vector2Int position)
    {
        if (position.x >= 0 && position.x < gridWidth && position.y >= 0 && position.y < gridHeight)
        
            return grid[position.x, position.y];
        
        return null;
    }
}

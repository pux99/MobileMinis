using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Minigames.Tetris.PlaceThePieces
{

    public class PtPGridManager : MonoBehaviour
    {
        public static PtPGridManager Instance { get; set; }
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
            _transform = GetComponent<RectTransform>();
            _gridLayout = GetComponent<GridLayoutGroup>();
        }
        
        
        [SerializeField] private Tile tile;
        private Tile[,] grid;
        
        private GridLayoutGroup _gridLayout;
        private RectTransform _transform;
        
        private Vector2 _tileSize = new Vector2(100, 100);
        
        [HideInInspector]public int gridWidth;
        [HideInInspector]public int gridHeight;

        
        public void MakeGrid(int x, int y)
        {
            gridWidth = x;
            gridHeight = y;
            
            GenerateGridLayout();
            GenerateGrid();
        }
        void GenerateGridLayout()
        {
            
            float gridWidthInPixels = _transform.rect.width;
            float gridHeightInPixels = _transform.rect.height;

            
            float tileWidth = gridWidthInPixels / gridWidth;
            float tileHeight = gridHeightInPixels / gridHeight;
            
            float tileSize = Mathf.Min(tileWidth, tileHeight);
            
            _tileSize = new Vector2(tileSize, tileSize);
            _gridLayout.cellSize = _tileSize;
            _gridLayout.constraintCount = gridWidth;
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

        public void CleanGrid()
        {
            foreach (var x in grid)
            {
                Destroy(x.gameObject);
            }
        }
        public Tile GetTileAtPosition(Vector2Int position)
        {
            if (position.x >= 0 && position.x < gridWidth && position.y >= 0 && position.y < gridHeight)

                return grid[position.x, position.y];

            return null;
        }
    }
}
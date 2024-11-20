using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Tetris.PlaceThePieces
{
    public class PtPGameManager : MonoBehaviour
    {
        //Singleton
        public static PtPGameManager Instance { get; private set; }
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
        
        
        //Game config
        [SerializeField] private int totalAmountOfPieces;
        [SerializeField] private Vector2Int gridSize;
        [HideInInspector] public int effectNumber; //Damage or shild the player will get
        
        //Drag variables
        public Image pieceImage { get; private set; }
        public Vector2Int[] OccupiedCells { get; private set; }
        private int _pieceSize;
        private bool isGameRunning = false;
        
        //Game Managing
        public event Action<int> OnPiecePlaced;
        public event Action WinMinigame; 
        public PtPGridManager _gridManager;
        
        
        public void StartMinigame()
        {
            OnPiecePlaced += OnPiecePlacedHandler;
            PtPGridManager.Instance.MakeGrid(gridSize.x, gridSize.y);
            PtPFactory.Instance.GenerateTetrisPieces(totalAmountOfPieces);
            isGameRunning = true;
        }

        public void ResetMinigame()
        {
            if (isGameRunning)
            { 
                OnPiecePlaced -= OnPiecePlacedHandler;
            PtPFactory.Instance.CleanContainer();
            PtPGridManager.Instance.CleanGrid();
            PointsCounter.Instance.CleanCounter();
            }
        }
        private void OnPiecePlacedHandler(int size)
        {
            PointsCounter.Instance.AddPoints(size);
        }
        
        public void SetPieceInfo(Image image, Vector2Int[] occupiedCells, int size)
        {
            pieceImage = image;
            OccupiedCells = occupiedCells;
            _pieceSize = size;
        }
        public void ClearOccupiedCells()
        {
            OccupiedCells = null;
            _pieceSize = 0;
        }
        public void ColorNeighboringTiles(Vector2Int position, Color color)
        {
            if (OccupiedCells == null) return;

            foreach (Vector2Int cell in OccupiedCells)
            {
                Tile tile = _gridManager.GetTileAtPosition(position + cell);
                if (tile != null && !tile.IsOccupied())
                {
                    tile.SetColor(color);
                }
            }
        }
        public void PlacePiece(Vector2Int position)
        {
            if (OccupiedCells == null || !IsDropValid(position)) return;

            foreach (Vector2Int cell in OccupiedCells)
            {
                Tile tile = _gridManager.GetTileAtPosition(position + cell);
                if (tile != null && !tile.IsOccupied())
                {
                    tile.SetOccupied();
                }
            }

            OnPiecePlaced?.Invoke(_pieceSize);
        }
        private bool IsDropValid(Vector2Int position)
        {
            if (OccupiedCells == null) return false;

            foreach (Vector2Int cell in OccupiedCells)
            {
                Vector2Int targetPosition = position + cell;
                if (targetPosition.x < 0 || targetPosition.x >= _gridManager.gridWidth ||
                    targetPosition.y < 0 || targetPosition.y >= _gridManager.gridHeight)
                {
                    return false;
                }

                Tile tile = _gridManager.GetTileAtPosition(position + cell);
                if (tile.IsOccupied())
                {
                    return false;
                }
            }

            return true;
        }

        public void FinnishMinigame()
        {
            effectNumber = PointsCounter.Instance.currentCount;
            WinMinigame?.Invoke();
        }

        public void DisableMinigame()
        {
            gameObject.SetActive(false);
        }
    }
}

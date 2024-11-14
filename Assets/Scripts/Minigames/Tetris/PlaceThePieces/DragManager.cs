using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Tetris.PlaceThePieces
{
    public class DragManager : MonoBehaviour
    {
        public static DragManager Instance { get; private set; }
        public Image DraggedImage { get; private set; }
        public Vector2Int[] OccupiedCells { get; private set; }

        public event Action OnPiecePlaced;

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

        public void SetPieceInfo(Image image, Vector2Int[] occupiedCells)
        {
            DraggedImage = image;
            OccupiedCells = occupiedCells;
        }

        public void ClearOccupiedCells()
        {
            OccupiedCells = null;
        }

        public void ColorNeighboringTiles(Vector2Int position, Color color)
        {
            if (OccupiedCells == null) return;

            foreach (Vector2Int cell in OccupiedCells)
            {
                Tile tile = GridManager.Instance.GetTileAtPosition(position + cell);
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
                Tile tile = GridManager.Instance.GetTileAtPosition(position + cell);
                if (tile != null && !tile.IsOccupied())
                {
                    tile.SetOccupied();
                }
            }

            OnPiecePlaced?.Invoke();
        }

        private bool IsDropValid(Vector2Int position)
        {
            if (OccupiedCells == null) return false;

            foreach (Vector2Int cell in OccupiedCells)
            {
                Vector2Int targetPosition = position + cell;
                if (targetPosition.x < 0 || targetPosition.x >= GridManager.Instance.gridWidth ||
                    targetPosition.y < 0 || targetPosition.y >= GridManager.Instance.gridHeight)
                {
                    return false;
                }

                Tile tile = GridManager.Instance.GetTileAtPosition(position + cell);
                if (tile.IsOccupied())
                {
                    return false;
                }
            }

            return true;
        }
    }
}

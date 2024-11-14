using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Minigames.Tetris.PlaceThePieces
{
    public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
    {
        private Image tileImage;
        private Color finalColor;
        private bool isOccupied;

        private PtPGameManager _gameManager;
        [SerializeField] public Vector2Int gridPosition;

        private void Start()
        {
            _gameManager = PtPGameManager.Instance;
            tileImage = GetComponent<Image>();
            isOccupied = false;
        }
        public bool IsOccupied()
        {
            return isOccupied;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isOccupied || _gameManager.OccupiedCells == null) return;
            _gameManager.ColorNeighboringTiles(gridPosition, Color.gray);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            if (isOccupied || _gameManager.OccupiedCells == null) return;
            _gameManager.ColorNeighboringTiles(gridPosition, Color.white);
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (isOccupied) return;
            _gameManager.ColorNeighboringTiles(gridPosition, Color.white);
            _gameManager.PlacePiece(gridPosition);
            _gameManager.ClearOccupiedCells();
        }
        public void SetOccupied()
        {
            isOccupied = true;
            tileImage.color = _gameManager.pieceImage.color;
        }
        public void SetColor(Color color)
        {
            if (!isOccupied)
            {
                tileImage.color = color;
            }
        }
        public void ResetTile()
        {
            isOccupied = false;
            tileImage.color = Color.white;
        }
    }
}

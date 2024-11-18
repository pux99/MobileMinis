using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Minigames.Tetris.PlaceThePieces
{
    public class DragPiece : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
    {
        private Canvas _canvas;
        [SerializeField] private Image image;
        private Vector3 _lastPosition;
        private Transform _lastParent;
        private PtPGameManager _gameManager;

        public Vector2Int[] occupiedCells;
        private int _pieceSize;

        private void OnEnable()
        {
            _canvas = FindObjectOfType<Canvas>();
            _gameManager = PtPGameManager.Instance;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastParent = transform.parent;
            image.raycastTarget = false;
            _lastPosition = transform.position;
            
            transform.SetParent(_canvas.transform);
            transform.SetAsLastSibling();

            _gameManager.SetPieceInfo(image, occupiedCells, _pieceSize);
            _gameManager.OnPiecePlaced += DeactivatePiece;
        }
        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, eventData.position, _canvas.worldCamera, out Vector2 position);
            transform.position = _canvas.transform.TransformPoint(position);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            transform.position = _lastPosition;
            transform.SetParent(_lastParent);

            _gameManager.ClearOccupiedCells();
            _gameManager.OnPiecePlaced -= DeactivatePiece;
        }
        public void SetPieceData(Vector2Int[] newOccupiedCells)
        {
            occupiedCells = newOccupiedCells;
            _pieceSize = newOccupiedCells.Length;
        }
        private void DeactivatePiece(int size)
        {
            gameObject.SetActive(false);
            _gameManager.OnPiecePlaced -= DeactivatePiece;
        }

    }
}
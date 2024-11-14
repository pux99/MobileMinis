using System;
using System.Collections;
using System.Collections.Generic;
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

        public Vector2Int[] occupiedCells;

        private void OnEnable()
        {
            _canvas = FindObjectOfType<Canvas>();
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastParent = transform.parent;
            image.raycastTarget = false;
            _lastPosition = transform.position;
            transform.SetParent(_canvas.transform);
            transform.SetAsLastSibling();

            DragManager.Instance.SetPieceInfo(image, occupiedCells);

            DragManager.Instance.OnPiecePlaced += DeactivatePiece;
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform,
                eventData.position, _canvas.worldCamera, out Vector2 position);
            transform.position = _canvas.transform.TransformPoint(position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            transform.position = _lastPosition;
            transform.SetParent(_lastParent);

            DragManager.Instance.ClearOccupiedCells();
            DragManager.Instance.OnPiecePlaced -= DeactivatePiece;
        }

        public void SetPieceData(Vector2Int[] newOccupiedCells)
        {
            occupiedCells = newOccupiedCells;
        }

        private void DeactivatePiece()
        {
            gameObject.SetActive(false);
            DragManager.Instance.OnPiecePlaced -= DeactivatePiece;
        }

    }
}
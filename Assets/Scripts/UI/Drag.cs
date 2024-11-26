using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tetris_Minigame.Scripts.UI
{
    public class Drag : MonoBehaviour,IEndDragHandler,IDragHandler,IBeginDragHandler
    {
        [SerializeField]private Canvas _canvas;

        public Canvas Canvas
        {
            get => _canvas;
            set => _canvas = value;
        }
        [SerializeField] private Image image;
        private Vector3 _lastPosition;
        private Transform _lastParent;

        private void Start()
        {
            //_canvas = FindObjectOfType<Canvas>();//Cambiar
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            RectTransform parentRect = _lastParent.GetComponent<RectTransform>();
            if(!parentRect.rect.Contains(transform.GetComponent<RectTransform>().localPosition+parentRect.position))
                transform.position = _lastPosition;
            transform.SetParent(_lastParent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            PointerEventData pointerEventData = eventData;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)_canvas.transform,
                pointerEventData.position,
                _canvas.worldCamera,
                out Vector2 position);
            transform.position = _canvas.transform.TransformPoint(position);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            _lastParent = transform.parent;
            image.raycastTarget = false;
            _lastPosition = transform.position;
            transform.SetParent(_canvas.transform);
            transform.SetAsLastSibling();
        }
    }
}

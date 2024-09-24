using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tetris_Minigame.Scripts.UI
{
    public class Drag : MonoBehaviour,IEndDragHandler,IDragHandler,IBeginDragHandler,IPointerClickHandler
    {
        private Canvas _canvas;
        [SerializeField] private Image _image;
        private Transform _lastPosition;
        private void OnEnable()
        {
            _canvas = FindObjectOfType<Canvas>();
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log(transform.position);
            _image.raycastTarget = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            PointerEventData pointerEventData = (PointerEventData)eventData;

            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)_canvas.transform,
                pointerEventData.position,
                _canvas.worldCamera,
                out position);
            transform.position = _canvas.transform.TransformPoint(position);
        
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _image.raycastTarget = false;
            _lastPosition = transform;
            transform.SetAsLastSibling();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //eventData.
        }
    }
}

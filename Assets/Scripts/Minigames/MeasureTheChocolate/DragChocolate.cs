using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Minigames.MeasureTheChocolate
{


    public class DragChocolate : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private Canvas _canvas;
        private Vector3 _originalPosition;
        private Transform _originalParent;
        private Transform _goalContainer;

        private void Start()
        {
            _canvas = FindObjectOfType<Canvas>();
            _goalContainer = MinigameManager.Instance.goalContainer;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _originalPosition = transform.position;
            _originalParent = transform.parent;

            transform.SetParent(_canvas.transform);
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform,
                eventData.position, _canvas.worldCamera, out Vector2 position);

            transform.position = _canvas.transform.TransformPoint(position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            // Check if dropped in the goal container
            if (IsOverGoalContainer())
            {
                transform.SetParent(_goalContainer);
                transform.localPosition = Vector3.zero;

                Chocolate chocolate = GetComponent<Chocolate>();
                MinigameManager.Instance.OnChocolateMovedToGoal(chocolate);
            }
            else
            {
                // Return to original position and parent
                transform.position = _originalPosition;
                transform.SetParent(_originalParent);
            }
        }

        private bool IsOverGoalContainer()
        {
            // Check if the mouse is over the goal container
            RectTransform rectTransform = _goalContainer.GetComponent<RectTransform>();
            return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition,
                _canvas.worldCamera);
        }
    }
}

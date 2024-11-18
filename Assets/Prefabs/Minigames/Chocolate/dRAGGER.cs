using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dRAGGER : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerClickHandler, IDragHandler
{
    private Canvas _canvas;
    private Vector3 _originalPosition;
    private Transform _originalParent;

    public void OnPointerClick(PointerEventData eventData)
    {
        _originalPosition = transform.position;
        _originalParent = transform.parent;
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, eventData.position, _canvas.worldCamera, out Vector2 position);
        transform.position = _canvas.transform.TransformPoint(position);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _originalPosition;
        transform.SetParent(_originalParent);
    }

}

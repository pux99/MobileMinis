using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour,IEndDragHandler,IDragHandler,IBeginDragHandler,IPointerClickHandler
{
    private Canvas _canvas;
    [SerializeField] private Image _image;
    private Transform _lastPosition;
    private Transform _lastParent;
    private void OnEnable()
    {
        _canvas = FindObjectOfType<Canvas>();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(transform.position);
        _image.raycastTarget = true;
        transform.SetParent(_lastParent);
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
        _lastParent = transform.parent;
        _image.raycastTarget = false;
        _lastPosition = transform;
        transform.SetParent(_canvas.transform);
        transform.SetAsLastSibling();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //eventData.
    }
}

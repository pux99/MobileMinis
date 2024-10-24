using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class DragPiece : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    private Canvas _canvas;
    [SerializeField] private Image image;
    private Vector3 _lastPosition;
    private Transform _lastParent;
    
    public Vector2 size;
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
        
        DragManager.Instance.SetPieceInfo(image, size, occupiedCells);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)_canvas.transform, eventData.position, _canvas.worldCamera, out Vector2 position);
        transform.position = _canvas.transform.TransformPoint(position);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        RectTransform parentRect = _lastParent.GetComponent<RectTransform>();
        if (!parentRect.rect.Contains(transform.GetComponent<RectTransform>().localPosition + parentRect.position)) transform.position = _lastPosition;
        transform.SetParent(_lastParent);
        
        DragManager.Instance.ClearPieceInfo();
    }
    
    public void SetPieceData(Vector2 newSize, Vector2Int[] newOccupiedCells)
    {
        size = newSize;
        occupiedCells = newOccupiedCells;
    }
}

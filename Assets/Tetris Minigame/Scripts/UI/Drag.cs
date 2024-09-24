using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Drag : MonoBehaviour,IEndDragHandler,IDragHandler,IBeginDragHandler
{
    private Canvas _canvas;
    [FormerlySerializedAs("_image")] [SerializeField] private Image image;
    private Vector3 _lastPosition;
    private Transform _lastParent;
    private void OnEnable()
    {
        _canvas = FindObjectOfType<Canvas>();
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

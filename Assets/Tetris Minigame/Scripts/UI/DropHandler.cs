using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour,IDropHandler
{
    public Action<GameObject> AddToContainer;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        dropped.transform.rotation=quaternion.identity;
        StartCoroutine(TurnOfRaycastTarget(dropped.GetComponent<Image>()));
        StartCoroutine(ForcingParent(dropped));
        AddToContainer?.Invoke(dropped);
    }
    IEnumerator ForcingParent(GameObject piece)
    {
        yield return 0;
        piece.transform.SetParent(this.transform);
    }
    IEnumerator  TurnOfRaycastTarget(Image Piece)
    {
        yield return 0;
        Piece.raycastTarget = false;
    }
}

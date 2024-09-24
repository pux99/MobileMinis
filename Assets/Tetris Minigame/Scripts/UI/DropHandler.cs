using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Tetris_Minigame.Scripts.UI
{
    public class DropHandler : MonoBehaviour,IDropHandler
    {
        public Action<GameObject> AddToContainer;
        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            Debug.Log(dropped);
            dropped.transform.SetParent(this.transform);
            dropped.transform.rotation=quaternion.identity;
            StartCoroutine(TurnOfRaycastTarget(dropped.GetComponent<Image>()));
            AddToContainer?.Invoke(dropped);
        }

        IEnumerator  TurnOfRaycastTarget(Image Piece)
        {
            yield return 0;
            Piece.raycastTarget = false;
        }
    }
}

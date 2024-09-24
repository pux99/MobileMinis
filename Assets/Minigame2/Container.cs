using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Minigame2
{
    public class Container : MonoBehaviour
    {
        private Stack<GameObject> _pieces = new Stack<GameObject>();
        [SerializeField] private int maxCapacity = 3; // Max capacity of the container.
        
        public bool CanMovePiece()
        {
            return _pieces.Count > 0; // Check if there is at least one piece. 
        }
        public GameObject GetTopPiece()
        {
            return _pieces.Count > 0 ? _pieces.Peek() : null; // See the piece at the top.
        }
        public bool CanAddPiece()
        {
            return _pieces.Count < maxCapacity; // Check if there is room for the piece.
        }
        public void AddPiece(GameObject piece)
        {
            if (!CanAddPiece()) return;
            _pieces.Push(piece);
            piece.transform.SetParent(this.transform); //Changes the parent of the piece.
            piece.GetComponent<RectTransform>().localScale = Vector3.one;
        }
        public GameObject RemoveTopPiece()
        {
            return CanMovePiece() ? _pieces.Pop() : null; //Remove te top piece.
        }
    }
}

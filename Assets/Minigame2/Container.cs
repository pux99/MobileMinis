using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minigame2
{
    public class Container : MonoBehaviour
    {
        private Stack<GameObject> _pieces = new Stack<GameObject>();
        public int capacity = 4; // Max pieces the container can hold

        public bool CanMovePiece()
        {
            return _pieces.Count > 0;
        }

        public GameObject GetTopPiece()
        {
            return _pieces.Count > 0 ? _pieces.Peek() : null;
        }

        public bool CanAddPiece()
        {
            return _pieces.Count < capacity;
        }

        public void AddPiece(GameObject piece)
        {
            if (CanAddPiece())
            {
                _pieces.Push(piece);
            }
        }
        
        public GameObject RemoveTopPiece()
        {
            return CanMovePiece() ? _pieces.Pop() : null;
        }
    }
}

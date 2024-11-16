using System;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

namespace Minigames.Tetris.OrderThePieces
{
    public class Container : MonoBehaviour
    {
        private TetrisStack _pieces = new TetrisStack(3);
        [SerializeField] private int maxCapacity = 3;
        

        public bool CanMovePiece()
        {
            return _pieces.Count() > 0; // Check if there is at least one piece. 
        }
        public GameObject GetTopPiece()
        {
            return _pieces.Count() > 0 ? _pieces.Peek() : null; // See the piece at the top.
        }
        public bool CanAddPiece()
        {
            return _pieces.Count() < maxCapacity; // Check if there is room for the piece.
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
        public void ClearContainer()
        {
            if(_pieces.Count()>0)
                _pieces.Clear();
        }
    }
}

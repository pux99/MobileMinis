using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris_Minigame.Scripts.Tetris
{
    [CreateAssetMenu(fileName = "ListOfBasePieces", menuName = "Tetris/List of base pieces", order = 1)]
    public class SO_GruopOfBaseTetrisPieces : ScriptableObject
    {
        [Serializable]
        public class Piece
        {
            public Sprite Sprite;
            public Vector2 Size;
        }
        [SerializeField]private List<Piece> _pieces;
        public List<Piece> Pieces { get=> _pieces; }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ListOfBasePieces", menuName = "Tetris/List of base pieces", order = 1)]
public class SO_GruopOfBaseTetrisPieces : ScriptableObject
{
    [Serializable]
    public class Piece
    {
        public Sprite sprite;
        public Vector2 size;
    }
    [SerializeField]private List<Piece> pieces;
    public List<Piece> Pieces { get=> pieces; }
}

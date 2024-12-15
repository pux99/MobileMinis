using System.Collections.Generic;
using UnityEngine;

namespace Minigames.Tetris.General
{
    [CreateAssetMenu(fileName = "ListOfBasePieces", menuName = "Tetris/List of base pieces", order = 1)]
    public class SO_GruopOfBaseTetrisPieces : ScriptableObject
    {
        [SerializeField]private List<PieceData> pieces;
        public List<PieceData> Pieces { get=> pieces; }
    }
}

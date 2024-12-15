using UnityEngine;

namespace Minigames.Tetris.General
{
    [CreateAssetMenu(fileName = "PieceData", menuName = "Tetris/PieceData", order = 1)]
    public class PieceData :ScriptableObject
    {
        public Sprite sprite;
        public Sprite ColorSprite;
        public Vector2 size;
        public Vector2Int[] occupiedCells;
    }
}
using System.Collections;
using System.Collections.Generic;
using Tetris_Minigame.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Tetris.PlaceThePieces
{
    public class TetrisFactory : MonoBehaviour
    {
        [SerializeField] private GameObject tetrisLikePiece;
        [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
        [SerializeField] private SO_GroupOfColors groupOfColors;

        public GameObject CreateRandomTetrisPiece()
        {
            GameObject newPiece = Instantiate(tetrisLikePiece);


            AdvancedImage image = newPiece.GetComponent<AdvancedImage>();
            image.raycastTarget = true;

            SO_GruopOfBaseTetrisPieces.Piece data =
                groupOfBaseTetrisPieces.Pieces[Random.Range(0, groupOfBaseTetrisPieces.Pieces.Count)];

            image.sprite = data.sprite;

            image.color = groupOfColors.Colors[Random.Range(0, groupOfColors.Colors.Count)];

            var dragData = newPiece.GetComponent<DragPiece>();
            dragData.SetPieceData(data.occupiedCells);

            return newPiece;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TetrisFactory_MG3 : MonoBehaviour
{
    [SerializeField] private GameObject tetrisLikePiece;
    [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
    [SerializeField] private SO_GroupOfColors groupOfColors;
    
    public GameObject CreateRandomTetrisPiece()
    {
        GameObject newPiece = Instantiate(tetrisLikePiece);
        
        Image pieceImage = newPiece.GetComponent<Image>();
        pieceImage.raycastTarget = true;
        
        SO_GruopOfBaseTetrisPieces.Piece data = groupOfBaseTetrisPieces.Pieces[Random.Range(0, groupOfBaseTetrisPieces.Pieces.Count)];
        pieceImage.sprite = data.sprite;
        pieceImage.color = groupOfColors.Colors[Random.Range(0, groupOfColors.Colors.Count)];

        var sizeData = newPiece.GetComponent<DragPiece>();
        sizeData.SetPieceData(data.size, data.occupiedCells);
        
        return newPiece;
    }
}

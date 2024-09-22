using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TetrisFactory : MonoBehaviour
{
    [SerializeField]private GameObject baseTertisPiece;
    [SerializeField] private Camera MainCamera;
    public GameObject CreateRandomTetrisPieceTetris(SO_GruopOfBaseTetrisPieces listOfPieces, SO_GroupOfColors listOfColors)
    {
        GameObject newPiece = Instantiate(baseTertisPiece);
        Image pieceImage = newPiece.GetComponent<Image>();
        SO_GruopOfBaseTetrisPieces.Piece _data = listOfPieces.Pieces[Random.Range(0, listOfPieces.Pieces.Count)];
        pieceImage.sprite = _data.Sprite;
        pieceImage.color = listOfColors.Colors[Random.Range(0, listOfColors.Colors.Count)];
        pieceImage.rectTransform.sizeDelta = new Vector2(_data.Size.x * Screen.currentResolution.width*.10f, _data.Size.y * Screen.currentResolution.width*.10f);
        return newPiece;
    }
}

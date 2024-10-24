using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TetrisFactory_MG3 : MonoBehaviour
{
    [SerializeField] private GameObject baseTetrisPiece;
    [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
    [SerializeField] private SO_GroupOfColors groupOfColors;
    
    [SerializeField] private float sizeMod=0.05f;
    
    public GameObject CreateRandomTetrisPiece()
    {
        GameObject newPiece = Instantiate(baseTetrisPiece);
        
        Image pieceImage = newPiece.GetComponent<Image>();
        SO_GruopOfBaseTetrisPieces.Piece data = groupOfBaseTetrisPieces.Pieces[Random.Range(0, groupOfBaseTetrisPieces.Pieces.Count)];
        pieceImage.sprite = data.sprite;
        pieceImage.color = groupOfColors.Colors[Random.Range(0, groupOfColors.Colors.Count)];
        pieceImage.rectTransform.sizeDelta = new Vector2(
            data.size.x * Screen.currentResolution.width*sizeMod,
            data.size.y * Screen.currentResolution.width*sizeMod);
        return newPiece;
    }
}

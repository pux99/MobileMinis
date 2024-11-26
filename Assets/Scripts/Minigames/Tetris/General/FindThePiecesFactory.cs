using System.Collections.Generic;
using Tetris_Minigame.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Tetris.General
{
    public class FindThePiecesFactory : MonoBehaviour
    {
        [SerializeField] private GameObject baseTetrisPiece;
        [SerializeField] private float sizeMod=0.05f;
        [SerializeField] private Canvas GameCanvas;
        public GameObject CreateRandomTetrisPiece(SO_GruopOfBaseTetrisPieces listOfPieces, SO_GroupOfColors listOfColors)
        {
            GameObject newPiece = Instantiate(baseTetrisPiece);
            Image pieceImage = newPiece.GetComponent<Image>();
            Image pieceColor = newPiece.transform.GetChild(0).GetComponent<Image>();
            SO_GruopOfBaseTetrisPieces.Piece data = listOfPieces.Pieces[Random.Range(0, listOfPieces.Pieces.Count)];
            pieceImage.sprite = data.sprite;
            pieceColor.sprite = data.ColorSprite;
            pieceColor.color = listOfColors.Colors[Random.Range(0, listOfColors.Colors.Count)];
            pieceImage.rectTransform.sizeDelta = new Vector2(
                data.size.x * Screen.currentResolution.width*sizeMod,
                data.size.y * Screen.currentResolution.width*sizeMod);
            newPiece.gameObject.GetComponent<Drag>().Canvas = GameCanvas;
            return newPiece;
        }

        public List<GameObject> RandomizePieces(
            SO_GruopOfBaseTetrisPieces listOfPieces,
            SO_GroupOfColors listOfColors,
            List<GameObject> pieces)
        {
            foreach (var piece in pieces)
            {
                Image pieceImage = piece.GetComponent<Image>();
                Image pieceColor = piece.transform.GetChild(0).GetComponent<Image>();
                SO_GruopOfBaseTetrisPieces.Piece data = listOfPieces.Pieces[Random.Range(0, listOfPieces.Pieces.Count)];
                pieceImage.sprite = data.sprite;
                pieceColor.sprite = data.ColorSprite;
                pieceColor.color = listOfColors.Colors[Random.Range(0, listOfColors.Colors.Count)];
                pieceImage.rectTransform.sizeDelta = new Vector2(
                    data.size.x * Screen.currentResolution.width*sizeMod,
                    data.size.y * Screen.currentResolution.width*sizeMod);
            }
            return pieces;
        }
    }
}

using System.Collections.Generic;
using Tetris_Minigame.Scripts.UI;
using Unity.VisualScripting;
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
            TetrisPiece newPiece = Instantiate(baseTetrisPiece).GetComponent<TetrisPiece>();
            newPiece.data = listOfPieces.Pieces[Random.Range(0, listOfPieces.Pieces.Count)];
            newPiece.Initialize(listOfColors.Colors[Random.Range(0, listOfColors.Colors.Count)],sizeMod,GameCanvas);
            return newPiece.GameObject();
        }

        public List<GameObject> RandomizePieces(
            SO_GruopOfBaseTetrisPieces listOfPieces,
            SO_GroupOfColors listOfColors,
            List<GameObject> pieces)
        {
            foreach (var piece in pieces)
            {
                TetrisPiece remadePiece= piece.GetComponent<TetrisPiece>();
                remadePiece.data = listOfPieces.Pieces[Random.Range(0, listOfPieces.Pieces.Count)];
                remadePiece.Initialize(listOfColors.Colors[Random.Range(0, listOfColors.Colors.Count)],sizeMod,GameCanvas);
            }
            return pieces;
        }
    }
}

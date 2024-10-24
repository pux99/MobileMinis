using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetris_MG3 : MonoBehaviour
{
    [SerializeField] private int totalAmountOfPieces;
    
    [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
    [SerializeField] private SO_GroupOfColors groupOfColors;
    [SerializeField] private TetrisFactory factory;
    
    private List<GameObject> pieces;
    
    [SerializeField] public RectTransform playableGrid;
    [SerializeField] public RectTransform playerContainer;

    public void GeneratePieces()
    {
        pieces = GenerateTetrisPieces(totalAmountOfPieces);
        PlacePiecesInContainer(pieces, playerContainer);
    }
    
    private List<GameObject> GenerateTetrisPieces(int amount)
    {
        List<GameObject> pieces = new List<GameObject>();
    
        for (int i = 0; i < amount; i++)
        {
            GameObject newPiece = factory.CreateRandomTetrisPiece(groupOfBaseTetrisPieces, groupOfColors);
            newPiece.GetComponent<Image>().raycastTarget = false;
            pieces.Add(newPiece);
        }

        return pieces;
    }

    private void PlacePiecesInContainer(List<GameObject> pieces, RectTransform container)
    {
        for (var i = 0; i < pieces.Count; i++)
        {
            var piece = pieces[i];
            GameObject instantiatedPiece = Instantiate(piece, container, true);
            instantiatedPiece.GetComponent<RectTransform>().localScale = Vector3.one;
            piece.transform.SetParent(container);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetris_MG3 : MonoBehaviour
{
    [SerializeField] private int totalAmountOfPieces;
    
    [SerializeField] private TetrisFactory_MG3 factory;

    [SerializeField] private RectTransform _grid;
    private List<GameObject> _pieces;
    
    private void Start()
    {
        GeneratePieces();
    }

    public void GeneratePieces()
    {
        _pieces = GenerateTetrisPieces(totalAmountOfPieces);
    }
    
    private List<GameObject> GenerateTetrisPieces(int amount)
    {
        _pieces = new List<GameObject>();
    
        for (int i = 0; i < amount; i++)
        {
            GameObject newPiece = factory.CreateRandomTetrisPiece();
            newPiece.transform.SetParent(_grid);
            newPiece.transform.position = Vector3.zero;
            //newPiece.transform.localScale = new Vector3(.5f,.5f,.5f);
            _pieces.Add(newPiece);
        }

        return _pieces;
    }
}

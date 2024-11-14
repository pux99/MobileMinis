using System.Collections;
using System.Collections.Generic;
using Tetris_Minigame.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Tetris.PlaceThePieces
{
    public class PtPFactory : MonoBehaviour
    {
        public static PtPFactory Instance { get; set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        
        [SerializeField] private GameObject tetrisLikePiece;
        [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
        [SerializeField] private SO_GroupOfColors groupOfColors;

        private List<GameObject> _pieces;

        
        public List<GameObject> GenerateTetrisPieces(int amount)
        {
            _pieces = new List<GameObject>();

            for (int i = 0; i < amount; i++)
            {
                GameObject newPiece = CreateRandomTetrisPiece();
                newPiece.transform.SetParent(this.transform);
                newPiece.transform.position = Vector3.zero;
                newPiece.transform.localScale = new Vector3(.75f, .75f, 1f);
                _pieces.Add(newPiece);
            }

            return _pieces;
        }
        
        private GameObject CreateRandomTetrisPiece()
        {
            GameObject newPiece = Instantiate(tetrisLikePiece);

            AdvancedImage image = newPiece.GetComponent<AdvancedImage>();
            image.raycastTarget = true;

            SO_GruopOfBaseTetrisPieces.Piece data = groupOfBaseTetrisPieces.Pieces[Random.Range(0, groupOfBaseTetrisPieces.Pieces.Count)];

            image.sprite = data.sprite;

            image.color = groupOfColors.Colors[Random.Range(0, groupOfColors.Colors.Count)];

            DragPiece dragData = newPiece.GetComponent<DragPiece>();
            dragData.SetPieceData(data.occupiedCells);

            return newPiece;
        }

        public void CleanContainer()
        {
            if (_pieces.Count <= 0) return;
            foreach (var x in _pieces)
            {
                Destroy(x);
            }
        }
    }
}


using System.Collections.Generic;
using Tetris_Minigame.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Minigames.Tetris.OrderThePieces
{ 
    public class OtPFactory : MonoBehaviour
    {
        public static OtPFactory Instance { get; set; }
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
        
        public void InitalizePieces(int amountOfPieces, RectTransform[] goalContainers, RectTransform[] playerContainers)
        {
            _pieces = GenerateTetrisPieces(amountOfPieces);
            
            PlacePiecesInContainers(goalContainers, _pieces);
            
            List<GameObject> duplicatedPieces = DuplicatePieces(_pieces);
            PlacePiecesInContainers(playerContainers, duplicatedPieces);
        }
        private List<GameObject> GenerateTetrisPieces(int amount)
        {
            _pieces = new List<GameObject>();
            for (int i = 0; i < amount; i++)
            {
                GameObject newPiece = Instantiate(tetrisLikePiece, transform,true);
                AdvancedImage image = newPiece.GetComponent<AdvancedImage>();
                image.raycastTarget = false;
                
                SO_GruopOfBaseTetrisPieces.Piece data = groupOfBaseTetrisPieces.Pieces[Random.Range(0, groupOfBaseTetrisPieces.Pieces.Count)];
                image.sprite = data.sprite;
                image.color = groupOfColors.Colors[Random.Range(0, groupOfColors.Colors.Count)];
                
                _pieces.Add(newPiece);
            }

            return _pieces;
        }
        private List<GameObject> DuplicatePieces(List<GameObject> originalPieces)
        {
            List<GameObject> duplicatedPieces = new List<GameObject>();

            foreach (var piece in originalPieces)
            {
                // Create an exact copy of the original piece
                GameObject duplicatedPiece = Instantiate(piece, transform, true);
                duplicatedPieces.Add(duplicatedPiece);
            }

            return duplicatedPieces;
        }
        private void PlacePiecesInContainers(RectTransform[] containers, List<GameObject> piecesToPlace)
        {
            List<int> availableSpots = new List<int>();
            
            for (int i = 0; i < containers.Length; i++)
            {
                availableSpots.Add(i);
                availableSpots.Add(i);
                availableSpots.Add(i);
            }
            
            Shuffle(availableSpots);
            for (int i = 0; i < piecesToPlace.Count; i++)
            {
                int randomContainerIndex = availableSpots[i % availableSpots.Count];
                Container container = containers[randomContainerIndex].GetComponent<Container>();
                GameObject piece = piecesToPlace[i];
                piece.transform.SetParent(containers[randomContainerIndex], false);
                piece.transform.localScale = Vector3.one;

                container.AddPiece(piece);
            }
            
        }
        private static void Shuffle<T>(List<T> ts) 
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var r = Random.Range(i, count);
                (ts[i], ts[r]) = (ts[r], ts[i]);
            }
        }
        
    }
}


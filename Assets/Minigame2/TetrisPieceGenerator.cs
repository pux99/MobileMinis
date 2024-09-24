using System.Collections.Generic;
using Tetris_Minigame.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame2
{ 
    public class TetrisPieceGenerator : MonoBehaviour
    {
        [SerializeField] private int totalAmountOfPieces = 7;
    
        [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
        [SerializeField] private SO_GroupOfColors groupOfColors;
        [SerializeField] private TetrisFactory factory;
        
        [SerializeField] public RectTransform[] goalContainers; // Goal_A, Goal_B, Goal_C
        [SerializeField] public RectTransform[] playerContainers; // Cont_A, Cont_B, Cont_C

        public void GeneratePieces()
        {
            List<GameObject> pieces = GenerateTetrisPieces(totalAmountOfPieces);
            PlacePiecesInContainers(pieces, goalContainers);
            Shuffle(pieces);
            PlacePiecesInContainers(pieces, playerContainers);
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
        private void PlacePiecesInContainers(List<GameObject> pieces, RectTransform[] containers)
        {
            List<int> availableSpots = new List<int>(); // Create a list to track available spots.
            
            for (int i = 0; i < containers.Length; i++) // Add 3 spots per container.
            {
                availableSpots.Add(i);
                availableSpots.Add(i);
                availableSpots.Add(i);
            }
            
            Shuffle(availableSpots);
            
            foreach (var piece in pieces) // Loop through each piece and place it in a container.
            {
                int randomContainerIndex = availableSpots[0]; // Random index of available spots.
                availableSpots.RemoveAt(0); // Remove the index from the list.
        
                // Place the piece in the selected container.
                GameObject instantiatedPiece = Instantiate(piece, containers[randomContainerIndex], true);
                instantiatedPiece.GetComponent<RectTransform>().localScale = Vector3.one;
                
                Container container = containers[randomContainerIndex].GetComponent<Container>();
                if (container != null)
                {
                    container.AddPiece(instantiatedPiece);
                }
            }
        }
        public static void Shuffle<T>(List<T> ts) 
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


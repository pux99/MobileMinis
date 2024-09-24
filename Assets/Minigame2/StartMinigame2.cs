using System.Collections;
using System.Collections.Generic;
using Tetris_Minigame.Scripts.Tetris;
using Tetris_Minigame.Scripts.Utilitys;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Minigame2
{
    public class StartMinigame2 : MonoBehaviour
    {
        private RectTransform _selectedContainer = null;
        [SerializeField] private RectTransform[] goalContainers; // Goal_A, Goal_B, Goal_C
        [SerializeField] private RectTransform[] playerContainers; // Cont_A, Cont_B, Cont_C
        private const int MaxPiecesPerContainer = 4;
    
        // The 3 GOAL containers
        [SerializeField] private RectTransform goalA;
        [SerializeField] private RectTransform goalB;
        [SerializeField] private RectTransform goalC;

        // The 3 PLAYABLE containers
        [SerializeField] private RectTransform containerA;
        [SerializeField] private RectTransform containerB;
        [SerializeField] private RectTransform containerC;
    
        // Required to create the Tetris pieces
        [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
        [SerializeField] private SO_GroupOfColors groupOfColors;
        [SerializeField] private TetrisFactory factory;
    
    
        [SerializeField] private Camera mainCamera;
        
        private Stack<GameObject>[] goalStacks = new Stack<GameObject>[3];
        private Stack<GameObject>[] containerStacks = new Stack<GameObject>[3];
        private Dictionary<RectTransform, int> containerIndices = new Dictionary<RectTransform, int>();

    
    
        private void Start()
        {
            List<GameObject> piecesForPlay = new List<GameObject>();// List of the Pieces we are going to use for the game.
            
            for (int i = 0; i < 3; i++)
            {
                goalStacks[i] = new Stack<GameObject>();
            }
            for (int i = 0; i < 3; i++)
            {
                containerStacks[i] = new Stack<GameObject>();
            }
            for (int i = 0; i < playerContainers.Length; i++)
            {
                containerIndices.Add(playerContainers[i], i);
            }
            
            
            
            RectTransform[] goalContainers = { goalA, goalB, goalC };// Array for the GoalContainers
            int[] goalContainerPieceCounts = { 0, 0, 0 }; // Track the number of pieces per GoalContainers
        
            // Loop through the pieces to create and assign them to a goal container
            for (int i = 0; i < 9; i++)
            {
                GameObject newPiece = factory.CreateRandomTetrisPieceTetris(groupOfBaseTetrisPieces, groupOfColors);
                piecesForPlay.Add(newPiece);

                for (int j = 0; j < goalContainers.Length; j++)
                {
                    if (goalContainerPieceCounts[j] < 3)
                    {
                        newPiece = Instantiate(newPiece, goalContainers[j], true);
                        newPiece.GetComponent<RectTransform>().localScale = Vector3.one;
                        goalContainerPieceCounts[j]++;
                        break;
                    }
                }
                //_goalPieces.Enqueue(newPiece); //Cambiar a Stack?
            }
            for (int j = 0; j < goalContainers.Length; j++)
            {
                // Assuming the pieces were assigned correctly, push them to the stack
                foreach (Transform child in goalContainers[j])
                {
                    goalStacks[j].Push(child.gameObject);
                }
            }
        
            RectTransform[] playableContainers = { containerA, containerB, containerC }; // Array for the containers
            int[] playableContainerPieceCounts = { 0, 0, 0 }; // Track the number of pieces per container
        
            Shuffle(piecesForPlay);
        
            // Loop through the pieces to create and assign them to a goal container
            foreach (var piece in piecesForPlay)
            {
                for (int j = 0; j < goalContainers.Length; j++)
                {
                    if (playableContainerPieceCounts[j] < 3)
                    {
                        Instantiate(piece, playableContainers[j]);  // Use Instantiate to clone properly
                        piece.GetComponent<RectTransform>().localScale = Vector3.one;  // Keep scale
                        playableContainerPieceCounts[j]++;
                        break;
                    }
                }
            }
            for (int j = 0; j < playableContainers.Length; j++)
            {
                foreach (Transform child in playableContainers[j])
                {
                    containerStacks[j].Push(child.gameObject);
                }
            }
        }
    
        public static void Shuffle<T>(List<T> ts) {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i) {
                var r = UnityEngine.Random.Range(i, count);
                (ts[i], ts[r]) = (ts[r], ts[i]);
            }
        }

        public void OnContainerClicked(RectTransform container)
        {
            if (_selectedContainer == null)
            {
                _selectedContainer = container;
                HighlightContainer(container, true); // Turn container yellow
            }
            else
            {
                // Get indices of the selected container and the target container
                int selectedIndex = containerIndices[_selectedContainer];
                int targetIndex = containerIndices[container];

                // Try to move the piece from the selected container to the target container
                //TryMovePiece(selectedIndex, targetIndex);

                HighlightContainer(_selectedContainer, highlight: false); // Reset color of the first container
                _selectedContainer = null; // Deselect the container
            }
        }
        private void HighlightContainer(RectTransform container, bool highlight)
        {
            Color highlightColor = highlight ? Color.yellow : Color.red;
            container.GetComponent<Image>().color = highlightColor; // Assuming containers have an Image component
        }

        private void TryMovePiece(Container sourceContainer, Container targetContainer)
        {
            // Check if the move is possible
            //if (targetContainer.HasSpace())
            {
                // Remove the top piece from the source container
                GameObject pieceToMove = sourceContainer.RemoveTopPiece();
                if (pieceToMove != null)
                {
                    // Add the piece to the target container
                    targetContainer.AddPiece(pieceToMove);
                }
            }
            //else
            {
                // Move not possible, reset the piece's position back to the source container
                Debug.Log("Move not possible, returning piece.");
            }
        }

        private IEnumerator ReturnPieceToOrigin(GameObject piece, RectTransform sourceContainer)
        {
            Vector3 originalPosition = piece.GetComponent<RectTransform>().anchoredPosition;

            float elapsedTime = 0f;
            float duration = 0.5f; // Smooth transition duration
            Vector3 startPos = piece.transform.position;
            Vector3 targetPos = sourceContainer.position;

            while (elapsedTime < duration)
            {
                piece.transform.position = Vector3.Lerp(startPos, targetPos, (elapsedTime / duration));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Return the piece to the original container
            piece.transform.SetParent(sourceContainer, worldPositionStays: false);
            piece.GetComponent<RectTransform>().anchoredPosition = originalPosition;
        }

        private void WinCon()
        {
            bool won = true;

            for (int i = 0; i < 3; i++)
            {
                if (goalStacks[i].Count != containerStacks[i].Count)
                {
                    won = false;
                    break;
                }
                // Compare pieces in order for both goal and player stacks
                GameObject[] goalArray = goalStacks[i].ToArray();
                GameObject[] playerArray = containerStacks[i].ToArray();

                for (int j = 0; j < goalArray.Length; j++)
                {
                    if (goalArray[j].name != playerArray[j].name) // Assuming the piece names are the same
                    {
                        won = false;
                        break;
                    }
                }
            }

            if (won)
            {
                Debug.Log("Player has won!");
                // Trigger any win behavior
            }
        }
    
    }
}

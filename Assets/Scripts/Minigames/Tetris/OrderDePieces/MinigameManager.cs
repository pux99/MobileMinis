using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

namespace Minigame2
{
    public class MinigameManager : MonoBehaviour
    {
        private TetrisPieceGenerator _pieceGenerator;
        
        [SerializeField] public RectTransform[] playerContainers;
        [SerializeField] public RectTransform[] goalContainers;
        [SerializeField] public Image shield;
        [SerializeField] public Animator animator;
        
        private Container _selectedContainer;
        [SerializeField] public RectTransform[] containers;
        
        private void Start()
        {
            _pieceGenerator = GetComponent<TetrisPieceGenerator>();
            if (_pieceGenerator != null)
            {
                _pieceGenerator.GeneratePieces();
                
                playerContainers = _pieceGenerator.playerContainers;
                goalContainers = _pieceGenerator.goalContainers;
            }
        }
        public void SelectContainer(Container container)
        {
            if (_selectedContainer == null)
            {
                if (!container.CanMovePiece()) return; // Can only select if there's a piece to move
                _selectedContainer = container;
                HighlightContainer(container);
            }
            else
            {
                TryMovePiece(_selectedContainer, container); // Try to move the top piece from the selected container to this container
                _selectedContainer = null;
                ResetHighlights();
                CheckWinCondition();
            }
        }
        private void TryMovePiece(Container fromContainer, Container toContainer)
        {
            if (fromContainer == null || toContainer == null || fromContainer == toContainer) return;
            var topPiece = fromContainer.GetTopPiece();

            if (topPiece != null && toContainer.CanAddPiece()) // Valid move
            {
                fromContainer.RemoveTopPiece(); 
                toContainer.AddPiece(topPiece);
            }
            else
            {
                Debug.Log("No room in the container.");
            }
        }
        private void HighlightContainer(Container container)
        {
            Image image = container.GetComponent<Image>();

            if (image != null)
            {
                image.color = Color.yellow;
                Debug.Log("Container highlighted");
            }
            else
            {
                Debug.LogError("Could not highlight");
            }
        }
        private void ResetHighlights()
        {
            foreach (RectTransform container in containers)
            {
                Image image = container.GetComponent<Image>();
                if (image != null)
                {
                    image.color = Color.white;
                }
            }
        }
        private void CheckWinCondition()
        {
            bool victory = true;
            
            for (int i = 0; i < playerContainers.Length; i++)
            {
                if (!DoesContainerMatchGoal(playerContainers[i], goalContainers[i]))//Compare pieces between containers.
                {
                    victory = false;
                    break;
                }
            }

            if (victory)
            {
                Debug.Log("Minijuego de defensa completado");
                //VICTORY ACA!
                shield.enabled = true;
                animator.SetTrigger("defensa");
                RestartMinigame();
            }
        }

        private bool DoesContainerMatchGoal(RectTransform playerContainer, RectTransform goalContainer)
        {
            if (playerContainer.childCount != goalContainer.childCount) //Consider amount of pieces.
            {
                return false; 
            }
            
            for (int i = 0; i < playerContainer.childCount; i++) //Compare pieces.
            {
                var playerPiece = playerContainer.GetChild(i).GetComponent<Image>();
                var goalPiece = goalContainer.GetChild(i).GetComponent<Image>();

                if (playerPiece == null || goalPiece == null || playerPiece.sprite != goalPiece.sprite)
                {
                    return false;
                }
            }

            return true;
        }

        void RestartMinigame()
        {
            _pieceGenerator.ResetGame();
        }
    }
}

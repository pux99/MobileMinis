using System;
using UnityEngine;
using UnityEngine.UI;
using Container = Minigames.Tetris.OrderThePieces.Container;

namespace Minigames.Tetris.OrderDePieces
{
    public class OtPManager : MonoBehaviour
    {
        public static OtPManager Instance { get; set; }
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
        
        public int amountOfPieces;
        
        [SerializeField] public RectTransform[] playerContainers;
        [SerializeField] public RectTransform[] goalContainers;
        public OtPFactory factory;
        
        private Container _selectedContainer;

        public event Action WinMinigame;
        public void StartMinigame()
        {
            factory.InitalizePieces(amountOfPieces, goalContainers, playerContainers);
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
            }
        }
        private void ResetHighlights()
        {
            foreach (RectTransform container in playerContainers)
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
                WinMinigame?.Invoke();
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

        public void ResetMinigame()
        {
            foreach (var container in playerContainers)
            {
                // Destroy all children (GameObjects)
                foreach (Transform child in container)
                {
                    Destroy(child.gameObject);
                }

                // Clear the internal _pieces stack in the container
                Container containerComponent = container.GetComponent<Container>();
                containerComponent?.ClearContainer();
            }

            foreach (var container in goalContainers)
            {
                // Destroy all children (GameObjects)
                foreach (Transform child in container)
                {
                    Destroy(child.gameObject);
                }

                // Clear the internal _pieces stack in the container
                Container containerComponent = container.GetComponent<Container>();
                containerComponent?.ClearContainer();
            }
        }

    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame2
{
    public class MinigameManager : MonoBehaviour
    {
        private TetrisPieceGenerator _pieceGenerator;
        private void Start()
        {
            _pieceGenerator = GetComponent<TetrisPieceGenerator>();
            if (_pieceGenerator != null)
            {
                _pieceGenerator.GeneratePieces(); // Call piece generation in Start
            }
        }

        private Container selectedContainer = null;
        [SerializeField] public RectTransform[] containers;

        public void SelectContainer(Container container)
        {
            if (selectedContainer == null)
            {
                selectedContainer = container;
                HighlightContainer(container);
            }
            else
            {
                TryMovePiece(selectedContainer, container);
                selectedContainer = null;
                ResetHighlights();
            }
        }

        public void TryMovePiece(Container fromContainer, Container toContainer)
        {
            if (fromContainer.CanMovePiece() && toContainer.CanAddPiece())
            {
                GameObject piece = fromContainer.RemoveTopPiece();
                toContainer.AddPiece(piece);
                CheckWinCondition();
            }
            else
            {
                Debug.Log("Invalid move"); // Falta agregar la logica de que vuelva a su contenedor
            }
        }
        
        private void CheckWinCondition()
        {
            // Check if all containers match their goal state
            // If so, trigger win condition
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
                Debug.LogError("No Image component found on the container!");
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
    }
}

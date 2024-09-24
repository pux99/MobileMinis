using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StartMinigame2 : MonoBehaviour
{
    private RectTransform selectedContainer = null;
    [SerializeField] private RectTransform[] goalContainers; // Goal_A, Goal_B, Goal_C
    [SerializeField] private RectTransform[] playerContainers; // Cont_A, Cont_B, Cont_C
    private const int maxPiecesPerContainer = 4;
    
    // The 3 GOAL containers
    [SerializeField] private RectTransform Goal_A;
    [SerializeField] private RectTransform Goal_B;
    [SerializeField] private RectTransform Goal_C;

    // The 3 PLAYABLE containers
    [SerializeField] private RectTransform Container_A;
    [SerializeField] private RectTransform Container_B;
    [SerializeField] private RectTransform Container_C;
    
    // Required to create the Tetris pieces
    [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
    [SerializeField] private SO_GroupOfColors groupOfColors;
    [SerializeField] private TetrisFactory factory;
    
    
    [SerializeField] private Camera MainCamera;
    
    private TetrisQueue _goalPieces=new TetrisQueue(10);// Hacerlo Stack
    private TetrisQueue _containerPieces=new TetrisQueue(10);// Hacerlo Stack
    
    
    private void Start()
    {
        // List of the Pieces we are going to use for the game.
        List<GameObject> piecesForPlay = new List<GameObject>();
        
       
        RectTransform[] goalContainers = { Goal_A, Goal_B, Goal_C };// Array for the GoalContainers
        int[] goalContainerPieceCounts = { 0, 0, 0 }; // Track the number of pieces per GoalContainers
        
        // Loop through the pieces to create and assign them to a goal container
        for (int i = 0; i < 9; i++)
        {
            GameObject newPiece = factory.CreateRandomTetrisPieceTetris(groupOfBaseTetrisPieces, groupOfColors);
            newPiece.GetComponent<RectTransform>().rotation = quaternion.Euler(new Vector3(0,0,Random.Range(0, 360)));
            piecesForPlay.Add(newPiece);

            for (int j = 0; j < goalContainers.Length; j++)
            {
                if (goalContainerPieceCounts[j] < 3)
                {
                    newPiece = Instantiate(newPiece, goalContainers[j], true);
                    newPiece.GetComponent<RectTransform>().rotation = quaternion.identity;
                    newPiece.GetComponent<RectTransform>().localScale = Vector3.one;
                    goalContainerPieceCounts[j]++;
                    break;
                }
            }
            //_goalPieces.Enqueue(newPiece); //Cambiar a Stack?
        }
        
        
        RectTransform[] playableContainers = { Container_A, Container_B, Container_C }; // Array for the containers
        int[] playableContainerPieceCounts = { 0, 0, 0 }; // Track the number of pieces per container
        
        Shuffle(piecesForPlay);
        
        // Loop through the pieces to create and assign them to a goal container
        foreach (var piece in piecesForPlay)
        {
            for (int j = 0; j < goalContainers.Length; j++)
            {
                if (playableContainerPieceCounts[j] < 3)
                {
                    piece.transform.SetParent(playableContainers[j]);
                    piece.GetComponent<RectTransform>().rotation = quaternion.identity;
                    piece.GetComponent<RectTransform>().localScale = Vector3.one;
                    playableContainerPieceCounts[j]++;
                    break;
                }
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

    private void OnContainerClicked(RectTransform container)
    {
        if (selectedContainer == null)
        {
            selectedContainer = container;
            HighlightContainer(container, true); // Turn container yellow
        }
        else
        {
            TryMovePiece(selectedContainer, container);
            HighlightContainer(selectedContainer, false); // Reset color of the first container
            selectedContainer = null; // Deselect the container
        }
    }
    private void HighlightContainer(RectTransform container, bool highlight)
    {
        Color highlightColor = highlight ? Color.yellow : Color.white;
        container.GetComponent<Image>().color = highlightColor; // Assuming containers have an Image component
    }

    private void TryMovePiece(RectTransform sourceContainer, RectTransform targetContainer)
    {
        if (sourceContainer.childCount == 0)return; //No piece in that container

        GameObject topPiece = sourceContainer.GetChild(sourceContainer.childCount - 1).gameObject; // Usar stack

        if (targetContainer.childCount < maxPiecesPerContainer)
        {
            // Moves the piece
            topPiece.transform.SetParent(targetContainer);
            //topPiece.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;  
            topPiece.GetComponent<RectTransform>().localScale = Vector3.one;

            CheckForWinCondition();
        }
        else
        {
            StartCoroutine(ReturnPieceToOrigin(topPiece, sourceContainer));
        }
    }

    private IEnumerator ReturnPieceToOrigin(GameObject piece, RectTransform sourceContainer)
    {
        Vector3 originalPosition = piece.GetComponent<RectTransform>().anchoredPosition;
        float elapsedTime = 0f;
        float duration = 1.0f;

        Vector3 startPos = piece.transform.position;
        Vector3 targetPos = sourceContainer.position;

        while (elapsedTime < duration)
        {
            piece.transform.position = Vector3.Lerp(startPos, targetPos, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
}

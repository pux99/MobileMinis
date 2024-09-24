using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class StartMinigame2 : MonoBehaviour
{
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
    
}

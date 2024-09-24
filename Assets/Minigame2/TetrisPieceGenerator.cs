using System.Collections;
using System.Collections.Generic;
using Tetris_Minigame.Scripts.Tetris;
using UnityEngine;

namespace Minigame2
{ 
    public class TetrisPieceGenerator : MonoBehaviour
{
    [SerializeField] private SO_GruopOfBaseTetrisPieces groupOfBaseTetrisPieces;
    [SerializeField] private SO_GroupOfColors groupOfColors;
    [SerializeField] private TetrisFactory factory;
    
    [SerializeField] private RectTransform[] goalContainers; // Goal_A, Goal_B, Goal_C
    [SerializeField] private RectTransform[] playerContainers; // Cont_A, Cont_B, Cont_C
    
    // The 3 GOAL containers
    [SerializeField] private RectTransform goalA;
    [SerializeField] private RectTransform goalB;
    [SerializeField] private RectTransform goalC;

    // The 3 PLAYABLE containers
    [SerializeField] private RectTransform containerA;
    [SerializeField] private RectTransform containerB;
    [SerializeField] private RectTransform containerC;
    
    public void GeneratePieces()
    {
        List<GameObject> piecesForPlay = new List<GameObject>(); // List of the Pieces we are going to use for the game.
        
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
}


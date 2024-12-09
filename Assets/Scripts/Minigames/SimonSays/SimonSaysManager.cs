using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSaysManager : MonoBehaviour
{
    public ColorSequenceManager sequenceManager;

    public void StartMinigame()
    {
        NextTurn();
    }

    public void NextTurn()
    {
        List<int> sequence = sequenceManager.GenerateNextInSequence();
        Debug.Log("Current sequence: " + string.Join(", ", sequence));
    }

    public void ResetGame()
    {
        sequenceManager.ResetSequence();
        Debug.Log("Game reset.");
    }
}

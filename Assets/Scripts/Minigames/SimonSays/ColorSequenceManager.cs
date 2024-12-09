using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSequenceManager : MonoBehaviour
{
    private List<int> sequence = new List<int>();
    private System.Random random = new System.Random();
    [SerializeField] private int maxSequenceLength = 8;
    
    public List<int> GenerateNextInSequence()
    {
        if (sequence.Count >= maxSequenceLength) ResetSequence();
        
        int nextNumber = random.Next(0, 4);
        sequence.Add(nextNumber);

        return new List<int>(sequence);
    }
    
    public void ResetSequence()
    {
        sequence.Clear();
    }
    
    public List<int> GetCurrentSequence()
    {
        return new List<int>(sequence);
    }
}

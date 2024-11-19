using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance { get; private set; }
    private void Awake()
    {
        // Ensure only one instance of MinigameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    
    [SerializeField] private ChocolateFactory factory;
    [SerializeField] public int cantChocolates;
    [SerializeField] public Transform goalContainer;
    
    private List<Chocolate> _generatedChocolates;
    private List<Chocolate> _goalChocolatesOrder;
    private void Start()
    {
        _generatedChocolates = factory.GenerateRandomChocolate(cantChocolates);
        _goalChocolatesOrder = new List<Chocolate>();
        StartMinigame();
    }

    public void OnChocolateMovedToGoal(Chocolate chocolate)
    {
        if (!_goalChocolatesOrder.Contains(chocolate))
        {
            _goalChocolatesOrder.Add(chocolate); // Add chocolate to the order list
        }

        // Check if the goal container is full
        if (goalContainer.childCount == cantChocolates)
        {
            CheckForVictory();
        }
    }
    
    private void CheckForVictory()
    {
        Debug.Log("Victory! Chocolates placed in the goal container:");
        for (int i = 0; i < _goalChocolatesOrder.Count; i++)
        {
            var chocolate = _goalChocolatesOrder[i];
            Debug.Log($"Order {i + 1}: Chocolate Bar - Columns = {chocolate.Columns}, Rows = {chocolate.Rows}, Size = {chocolate.Size}, Color = {chocolate.Color}");
        }
    }
    public void StartMinigame()
    {
        //Creo las piezas.
        // Defino la variable.
    }

}

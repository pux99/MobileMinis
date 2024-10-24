using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GridManager_MG3 : MonoBehaviour
{
    [SerializeField] private int size = 4;
    [SerializeField] private Tile tile;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                var spawnedTile = Instantiate(tile, transform);
                spawnedTile.name = $"Tile {x} {y}";
            }
        }
    }
}

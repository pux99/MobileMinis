using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using ManagerScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class MazeManager : MonoBehaviour
{
    [Header("Prefabs")][SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerPrefab;
    
    private GameObject _player;
    private GameObject _enemy;
    
    [Header("Factory")]public MazeFactory mazeFactory;
    
    //Maze Size
    [Header("Maze size")]
    [SerializeField]private int sizeX;
    [SerializeField]private int sizeY;
    
    //Enemy pathing
    private List<int> _shortestPath;
    private List<Vector3> _enemyPath;
    private bool _runnersAlive = false;
    
    //Events
    public event Action WinMinigame;
    public event Action LostMinigame;
    
    //Singleton
    public static MazeManager Instance { get; private set; }
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
    
    public void StartMinigame()
    {
        mazeFactory.MakeGrid(sizeX, sizeY);
        StartCoroutine(InitializeMinigameSequence());
    }
    private IEnumerator InitializeMinigameSequence()
    {
        yield return StartCoroutine(mazeFactory.CreateMaze());
        
        mazeFactory.SetFinnishLine();
        
        StartRace();
    }
    void StartRace()
    {
        GetEnemyPath();
        SetRunners(); 
    }
    private void GetEnemyPath()
    {
        int startNode = mazeFactory.Vect2Vert(new Vector2Int(0, 0));;
        int endNode = mazeFactory.Vect2Vert(new Vector2Int(sizeX -1 , sizeY -1));
        
        _shortestPath = Dijkstra(mazeFactory.MAdy, startNode, endNode);
        
        _enemyPath = GetOffsetPath(_shortestPath);
    }
    private void SetRunners()
    {
        if (!_runnersAlive)
        {
            _player = Instantiate(playerPrefab, this.transform, true);
            _player.transform.localScale *= mazeFactory.scaleFactor;
            
            _enemy = Instantiate(enemyPrefab, this.transform, true);
            _enemy.transform.localScale *= mazeFactory.scaleFactor;
            _runnersAlive = true;
        }
        var pos = new Vector3(mazeFactory.Rooms[0,0].transform.position.x + (mazeFactory.roomSize/2), mazeFactory.Rooms[0,0].transform.position.y + (mazeFactory.roomSize/2), 0);
        _player.SetActive(true);
        _enemy.SetActive(true);
        
        _player.transform.position = new Vector3(pos.x, pos.y, -2);
        _enemy.transform.position = new Vector3(pos.x, pos.y, -2);
        
        _enemy.GetComponent<EnemyMovement>().SetPath(_enemyPath);
    }
    public void EndGame(bool victory)
    {
        if (victory)
        {
            WinMinigame?.Invoke();
        }
        else
        {
            LostMinigame?.Invoke();
        }
        _player.SetActive(false);
        _enemy.SetActive(false);
    }
    public void ResetMinigame()
    {
        if (gameObject.activeInHierarchy)
        {
            mazeFactory.ResetMaze();
            StartCoroutine(InitializeMinigameSequence());
        }
    }
    
    //Utility & Dijkstra
    private static List<int> Dijkstra(int[,] matrix, int startNode, int endNode)
    {
        int totalNodes = matrix.GetLength(0);
        int[] distances = new int[totalNodes];
        bool[] visited = new bool[totalNodes];
        int[] previous = new int[totalNodes];
        List<int> path = new List<int>();
        
        for (int i = 0; i < totalNodes; i++)
        {
            distances[i] = int.MaxValue;
            previous[i] = -1;
            visited[i] = false;
        }
        distances[startNode] = 0;
        
        for (int i = 0; i < totalNodes; i++)
        {
            int currentNode = -1;
            int minDistance = int.MaxValue;
            for (int j = 0; j < totalNodes; j++)
            {
                if (!visited[j] && distances[j] < minDistance)
                {
                    currentNode = j;
                    minDistance = distances[j];
                }
            }
                
            // No more nodes to process
            if (currentNode == -1) break;

            visited[currentNode] = true;

            // Update distances to neighboring nodes
            for (int neighbor = 0; neighbor < totalNodes; neighbor++)
            {
                if (matrix[currentNode, neighbor] > 0 && !visited[neighbor])
                {
                    int newDist = distances[currentNode] + matrix[currentNode, neighbor];
                    if (newDist < distances[neighbor])
                    {
                        distances[neighbor] = newDist;
                        previous[neighbor] = currentNode;
                    }
                }
            }
        }

        // Reconstruct the shortest path
        for (int at = endNode; at != -1; at = previous[at])
        {
            path.Insert(0, at);
        }

        // Check if path exists
        if (path.Count == 0 || path[0] != startNode)
        {
            Debug.Log("No path found!");
            return null;
        }

        return path;
    }
    private List<Vector3> GetOffsetPath(List<int> path)
    {
        List<Vector3> offsetPath = new List<Vector3>();

        foreach (int index in path)
        {
            int x = index / sizeY;
            int y = index % sizeY;
            Room room = mazeFactory.Rooms[x, y];
            
            if (room != null)
            {
                Vector3 originalPosition = room.transform.position;
                Vector3 offsetPosition = originalPosition + new Vector3(mazeFactory.roomSize / 2, mazeFactory.roomSize / 2, 0);
                offsetPosition.z = -2;
                offsetPath.Add(offsetPosition);
            }
        }

        return offsetPath;
    }
    private void OnDrawGizmos()
    {
        if (_enemyPath == null || _enemyPath.Count < 2) return;

        Gizmos.color = Color.blue;

        for (int i = 0; i < _enemyPath.Count - 1; i++)
        {
            Vector3 currentPos = _enemyPath[i];
            Vector3 nextPos = _enemyPath[i + 1];

            Gizmos.DrawLine(currentPos, nextPos);
        }
    }
}

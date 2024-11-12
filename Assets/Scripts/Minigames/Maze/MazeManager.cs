using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MazeManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerPrefab;
    private GameObject _player;
    private GameObject _enemy;
    
    //Maze Size
    [SerializeField]private int _sizeX;
    [SerializeField]private int _sizeY;
    
    //Enemy pathing
    private List<int> _shortestPath;
    private List<Vector3> _enemyPath;
    private bool RunnersAlive = false;
    
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //First things first
    private bool _grid = false;
    public IEnumerator InitializeMinigameSequence()
    {
        if (!_grid)
        {
            yield return StartCoroutine(GenerateGrid());
            _grid = true;
        }
        yield return StartCoroutine(GenerateMaze());
        StartMinigame();
    }
    
    private IEnumerator GenerateGrid()
    {
        yield return StartCoroutine(MazeFactory.Instance.MakeGrid(_sizeX, _sizeY));
    }

    private IEnumerator GenerateMaze()
    {
        yield return StartCoroutine(MazeFactory.Instance.CreateMaze());
        MazeFactory.Instance.SetFinnishLine();
    }
    
    //Methods
    private void StartMinigame()
    {
        GetEnemyPath();
        SetRunners();
    }
    public void ResetMinigame()
    {
        MazeFactory.Instance.Reset();
        StartCoroutine(MazeFactory.Instance.CreateMaze());
    }
    private void GetEnemyPath()
    {
        int[,] mAdy = MazeFactory.Instance.CreateAdjacencyMatrix();
        
        int startNode = 0;
        int endNode = _sizeX * _sizeY - 1;
        
        _shortestPath = Dijkstra(mAdy, startNode, endNode);
        
        _enemyPath = GetOffsetPath(_shortestPath);
    }
    private void SetRunners()
    {
        if (!RunnersAlive)
        {
            _player = Instantiate(playerPrefab, this.transform, true);
            _player.transform.localScale *= MazeFactory.Instance.scaleFactor;
            
            _enemy = Instantiate(enemyPrefab, this.transform, true);
            _enemy.transform.localScale *= MazeFactory.Instance.scaleFactor;
            RunnersAlive = true;
        }
        var pos = new Vector3(MazeFactory.Instance.rooms[0,0].transform.position.x + (MazeFactory.Instance.roomSize/2), MazeFactory.Instance.rooms[0,0].transform.position.y + (MazeFactory.Instance.roomSize/2), 0);
        
        _player.transform.position = pos;
        
        _enemy.transform.position = pos;
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
    }
    
    //Utility & Dijkstra
    private List<int> Dijkstra(int[,] adjacencyMatrix, int startNode, int endNode)
    {
        int totalNodes = adjacencyMatrix.GetLength(0);
        int[] distances = new int[totalNodes];
        bool[] visited = new bool[totalNodes];
        int[] previous = new int[totalNodes];
        List<int> path = new List<int>();

        // Initialize distances and previous nodes
        for (int i = 0; i < totalNodes; i++)
        {
            distances[i] = int.MaxValue;
            previous[i] = -1;
            visited[i] = false;
        }
        distances[startNode] = 0;

        // Priority queue logic: find the node with the smallest distance
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
                if (adjacencyMatrix[currentNode, neighbor] > 0 && !visited[neighbor])
                {
                    int newDist = distances[currentNode] + adjacencyMatrix[currentNode, neighbor];
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
    private void OnDrawGizmos()
    {
        if (_shortestPath == null || _shortestPath.Count < 2) return;

        Gizmos.color = Color.blue; // Set the color for the path

        for (int i = 0; i < _shortestPath.Count - 1; i++)
        {
            int currentIndex = _shortestPath[i];
            int nextIndex = _shortestPath[i + 1];

            // Convert indices back to (x, y) positions
            int currentX = currentIndex / _sizeY;
            int currentY = currentIndex % _sizeY;
            int nextX = nextIndex / _sizeY;
            int nextY = nextIndex % _sizeY;

            Room currentRoom = MazeFactory.Instance.rooms[currentX, currentY];
            Room nextRoom = MazeFactory.Instance.rooms[nextX, nextY];

            if (currentRoom != null && nextRoom != null)
            {
                Vector3 currentPos = currentRoom.transform.position;
                Vector3 nextPos = nextRoom.transform.position;

                // Draw line between the current and next positions
                Gizmos.DrawLine(currentPos, nextPos);
            }
        }
    }
    private List<Vector3> GetOffsetPath(List<int> path)
    {
        List<Vector3> offsetPath = new List<Vector3>();

        foreach (int index in path)
        {
            int x = index / _sizeX;
            int y = index % _sizeY;
            Room room = MazeFactory.Instance.rooms[x, y];
            
            if (room != null)
            {
                Vector3 originalPosition = room.transform.position;
                Vector3 offsetPosition = originalPosition + new Vector3(MazeFactory.Instance.roomSize / 2, MazeFactory.Instance.roomSize / 2, 0);
                offsetPath.Add(offsetPosition);
            }
        }

        return offsetPath;
    }
}

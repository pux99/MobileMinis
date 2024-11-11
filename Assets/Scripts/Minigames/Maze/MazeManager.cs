using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    public int sizeX;
    public int sizeY;
    
    private List<int> shortestPath;
    private IEnumerator Start()
    {
        yield return StartCoroutine(MazeFactory.Instance.MakeMaze(sizeX, sizeY));
        int[,] MAdy = MazeFactory.Instance.CreateAdjacencyMatrix();
        
        int startNode = 0;
        int endNode = sizeX * sizeY - 1;
        
        shortestPath = Dijkstra(MAdy, startNode, endNode);
        
        List<Vector3> enemyPath = GetOffsetPath(shortestPath);
        enemy.GetComponent<EnemyMovement>().SetPath(enemyPath);
    }
    
    public List<int> Dijkstra(int[,] adjacencyMatrix, int startNode, int endNode)
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
        if (shortestPath == null || shortestPath.Count < 2) return;

        Gizmos.color = Color.green; // Set the color for the path

        for (int i = 0; i < shortestPath.Count - 1; i++)
        {
            int currentIndex = shortestPath[i];
            int nextIndex = shortestPath[i + 1];

            // Convert indices back to (x, y) positions
            int currentX = currentIndex / sizeY;
            int currentY = currentIndex % sizeY;
            int nextX = nextIndex / sizeY;
            int nextY = nextIndex % sizeY;

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
            int x = index / sizeX;
            int y = index % sizeY;
            Room room = MazeFactory.Instance.rooms[x, y];
            
            if (room != null)
            {
                Vector3 originalPosition = room.transform.position;
                Vector3 offsetPosition = originalPosition + new Vector3(MazeFactory.Instance.roomWidth / 2, MazeFactory.Instance.roomHeight / 2, 0);
                offsetPath.Add(offsetPosition);
            }
        }

        return offsetPath;
    }
}

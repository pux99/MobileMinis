using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class MazeFactory : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;
    Camera _cam;
    public float roomSize;
    
    //The grid
    public Room[,] rooms = null;
    
    //Size
    private int _numX = 10;
    private int _numY = 10;
    
    //Each RoomSize
    [HideInInspector] public float xOffset;
    [HideInInspector] public float yOffset;
    
    //Stack for backtracking (depth-first search)
    private Stack<Room> stack = new Stack<Room>();

    //To not break while making a maze.
    private bool _generating = false;
    
    //Singleton
    public static MazeFactory Instance { get; private set; }
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
        _cam =  Camera.main;
    }
    
    public IEnumerator MakeGrid(int sizeX, int sizeY)
    {
        _numX = sizeX;
        _numY = sizeY;
        GetRoomSize();
        SetOffset();
        yield return StartCoroutine(MakeGridForMaze());
    }
    
    //Making the GRID
    private IEnumerator MakeGridForMaze()
    {
        rooms = new Room[_numX, _numY];

        for (int i = 0; i < _numX; i++)
        {
            for (int j = 0; j < _numY; j++)
            {
                GameObject room = Instantiate(roomPrefab, this.transform, true);
                room.transform.position = new Vector3( (i * roomSize) + xOffset, (j * roomSize) - yOffset, 0.0f);
                
                room.name = "Room_" + i.ToString() + "_" + j.ToString();
                rooms[i, j] = room.GetComponent<Room>();
                rooms[i, j].Index = new Vector2Int(i, j);
            }   
        } 
        yield return null;
    }
    private void GetRoomSize()
    {
        float screenWidthWorld = _cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, _cam.nearClipPlane)).x - _cam.ScreenToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane)).x;
        float screenHeightWorld = (_cam.ScreenToWorldPoint(new Vector3(0, Screen.height, _cam.nearClipPlane)).y - _cam.ScreenToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane)).y) *.6f;

        Debug.Log("screenWidthWorld: " + screenWidthWorld);
        Debug.Log("screenHeightWorld" + screenHeightWorld);
        
        float targetRoomWidth = screenWidthWorld / _numX;
        float targetRoomHeight = screenHeightWorld / _numY;
        float targetRoomSize = Mathf.Min(targetRoomWidth, targetRoomHeight);
        
        float currentRoomSize = roomPrefab.transform.Find("Floor").GetComponent<SpriteRenderer>().bounds.size.x;
        
        float scaleFactor = targetRoomSize / currentRoomSize;
        
        roomPrefab.transform.localScale = new Vector3(roomPrefab.transform.localScale.x * scaleFactor, roomPrefab.transform.localScale.y * scaleFactor, 1);
        roomSize = targetRoomSize;
    }
    private void SetOffset()
    {
        xOffset = -(_numX * roomSize) / 2f;
        
        float screenHeightWorld = _cam.ScreenToWorldPoint(new Vector3(0, Screen.height, _cam.nearClipPlane)).y - _cam.ScreenToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane)).y;
        yOffset = (screenHeightWorld / 3f) + roomSize/3f;
    }

    //Generating Random Maze
    public IEnumerator CreateMaze()
    {
        if (_generating) yield break;
        Reset();
        
        stack.Push(rooms[0,0]);

        while (stack.Count > 0)
        {
            GenerateStep();
            yield return null;
        }

        _generating = false;
    }
    private void RemoveRoomWall(int x, int y, Room.Directions dir)
    {
        if (dir != Room.Directions.None)
        {
            rooms[x,y].SetDirFlag(dir, false);
        }

        Room.Directions opp = Room.Directions.None;
        switch (dir)
        {
            case Room.Directions.Top:
                if (y < _numY -1)
                {
                    opp = Room.Directions.Bottom;
                    ++y;
                }

                break;
            case Room.Directions.Right:
                if (x < _numX - 1)
                {
                    opp = Room.Directions.Left;
                    ++x;
                }

                break;
            case Room.Directions.Bottom:
                if (y > 0)
                {
                    opp = Room.Directions.Top;
                    --y;
                }

                break;
            case Room.Directions.Left:
                if (x > 0)
                {
                    opp = Room.Directions.Right;
                    --x;
                }

                break;
        }
        if (opp != Room.Directions.None)
        {
            rooms[x,y].SetDirFlag(opp,false);
        }
        
    }
    private void GenerateStep()
    {
        if (stack.Count == 0) return;

        Room currentRoom = stack.Peek();
        var neighbours = GetNeighboursNotVisited(currentRoom.Index.x, currentRoom.Index.y);

        if (neighbours.Count > 0)
        {
            var nextRoomInfo = neighbours[UnityEngine.Random.Range(0, neighbours.Count)];
            RemoveRoomWall(currentRoom.Index.x, currentRoom.Index.y, nextRoomInfo.Item1);

            Room nextRoom = nextRoomInfo.Item2;
            nextRoom.visited = true;
            stack.Push(nextRoom);
        }
        else
        {
            stack.Pop();
        }
    }
    private List<Tuple<Room.Directions, Room>> GetNeighboursNotVisited(int cx, int cy)
    {
        List<Tuple<Room.Directions, Room>> neighbours = new List<Tuple<Room.Directions, Room>>();

        foreach (Room.Directions dir in Enum.GetValues(typeof(Room.Directions)))
        {
            int x = cx;
            int y = cy;

            switch (dir)
            {
                case Room.Directions.Top:
                    if (y < _numY -1)
                    {
                        ++y;
                        if (!rooms[x,y].visited)
                        {
                         neighbours.Add(new Tuple<Room.Directions, Room>(Room.Directions.Top, rooms[x,y]));   
                        }
                    }

                    break;
                case Room.Directions.Right:
                    if (x < _numX -1)
                    {
                        ++x;
                        if (!rooms[x,y].visited)
                        {
                            neighbours.Add(new Tuple<Room.Directions, Room>(Room.Directions.Right, rooms[x,y]));   
                        }
                    }

                    break;
                case Room.Directions.Bottom:
                    if (y > 0)
                    {
                        --y;
                        if (!rooms[x,y].visited)
                        {
                            neighbours.Add(new Tuple<Room.Directions, Room>(Room.Directions.Bottom, rooms[x,y]));   
                        }
                    }

                    break;
                case Room.Directions.Left:
                    if (x > 0)
                    {
                        --x;
                        if (!rooms[x,y].visited)
                        {
                            neighbours.Add(new Tuple<Room.Directions, Room>(Room.Directions.Left, rooms[x,y]));   
                        }
                    }

                    break;
            }
        }
        return neighbours;
    }
    
    //Resets the Maze
    public void Reset()
    {
        foreach (var room in rooms)
        {
            room.visited = false;
            room.SetDirFlag(Room.Directions.Top, true);
            room.SetDirFlag(Room.Directions.Right, true);
            room.SetDirFlag(Room.Directions.Bottom, true);
            room.SetDirFlag(Room.Directions.Left, true);
        }
    }
    
    public int[,] CreateAdjacencyMatrix()
{
    int totalRooms = _numX * _numY;
    int[,] adjacencyMatrix = new int[totalRooms, totalRooms];

    // Initialize the matrix with 0s
    for (int i = 0; i < totalRooms; i++)
    {
        for (int j = 0; j < totalRooms; j++)
        {
            adjacencyMatrix[i, j] = 0;
        }
    }

    // Iterate through each room and check connectivity
    for (int x = 0; x < _numX; x++)
    {
        for (int y = 0; y < _numY; y++)
        {
            Room currentRoom = rooms[x, y];
            int currentIndex = (x * _numY) + y;

            // Check the top neighbor
            if (y < _numY - 1 && !currentRoom.GetDirFlag(Room.Directions.Top))
            {
                Room topNeighbor = rooms[x, y + 1];
                int topIndex = (x * _numY) + (y + 1);
                adjacencyMatrix[currentIndex, topIndex] = 1;
                adjacencyMatrix[topIndex, currentIndex] = 1; // For undirected graph
            }

            // Check the right neighbor
            if (x < _numX - 1 && !currentRoom.GetDirFlag(Room.Directions.Right))
            {
                Room rightNeighbor = rooms[x + 1, y];
                int rightIndex = ((x + 1) * _numY) + y;
                adjacencyMatrix[currentIndex, rightIndex] = 1;
                adjacencyMatrix[rightIndex, currentIndex] = 1;
            }

            // Check the bottom neighbor
            if (y > 0 && !currentRoom.GetDirFlag(Room.Directions.Bottom))
            {
                Room bottomNeighbor = rooms[x, y - 1];
                int bottomIndex = (x * _numY) + (y - 1);
                adjacencyMatrix[currentIndex, bottomIndex] = 1;
                adjacencyMatrix[bottomIndex, currentIndex] = 1;
            }

            // Check the left neighbor
            if (x > 0 && !currentRoom.GetDirFlag(Room.Directions.Left))
            {
                Room leftNeighbor = rooms[x - 1, y];
                int leftIndex = ((x - 1) * _numY) + y;
                adjacencyMatrix[currentIndex, leftIndex] = 1;
                adjacencyMatrix[leftIndex, currentIndex] = 1;
            }
        }
    }
    
    return adjacencyMatrix;
}
    public void SetFinnishLine()
    {
        rooms[_numX - 1, _numY - 1].SetFinnishLine();
    }

}

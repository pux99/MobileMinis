using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MazeFactory : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;
    
    //The grid
    public Room[,] rooms = null;
    
    //Size
    private int _numX = 10;
    private int _numY = 10;
    
    //Each RoomSize
    public float roomWidth;
    public float roomHeight;
    private float xOffset;
    private float yOffset;
    
    //Stack for backtracking (depth-first search)
    private Stack<Room> stack = new Stack<Room>();

    //To not break while making a maze.
    private bool generating = false;
    

    
    public IEnumerator MakeMaze(int sizeX, int sizeY)
    {
        _numX = sizeX;
        _numY = sizeY;
        GetRoomSize();
        SetOffset();
        yield return StartCoroutine(MakeGridForMaze());
        CreateMaze();
    }
    
    private void GetRoomSize()
    {
        SpriteRenderer[] spriteRenderers = roomPrefab.GetComponentsInChildren<SpriteRenderer>();
        Vector3 minBounds = Vector3.positiveInfinity;
        Vector3 maxBounds = Vector3.negativeInfinity;

        foreach (SpriteRenderer ren in spriteRenderers)
        {
            minBounds = Vector3.Min(minBounds, ren.bounds.max);
            maxBounds = Vector3.Max(maxBounds, ren.bounds.max);
        }

        roomWidth = maxBounds.x - minBounds.x;
        roomHeight = maxBounds.y - minBounds.y;
    }
    private void SetOffset()
    {
        xOffset = (_numX * roomWidth)/ 2 ;
        yOffset = (_numY * roomHeight) / 2;
    }
    private IEnumerator MakeGridForMaze()
    {
        rooms = new Room[_numX, _numY];

        for (int i = 0; i < _numX; i++)
        {
            for (int j = 0; j < _numY; j++)
            {
                GameObject room = Instantiate(roomPrefab, this.transform, true);
                room.transform.position = new Vector3( (i * roomWidth) - xOffset , (j * roomHeight) - yOffset, 0.0f);
                
                room.name = "Room_" + i.ToString() + "_" + j.ToString();
                rooms[i, j] = room.GetComponent<Room>();
                rooms[i, j].Index = new Vector2Int(i, j);
            }   
        } 
        yield return null;
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
    private bool GenerateStep()
    {
        if (stack.Count == 0) return true;

        Room r = stack.Peek();
        var neighbours = GetNeighboursNotVisited(r.Index.x, r.Index.y);

        if (neighbours.Count !=0 )
        {
            var index = 0;
            if (neighbours.Count > 1)
            {
                index = UnityEngine.Random.Range(0, neighbours.Count);
            }

            var item = neighbours[index];
            Room neighbour = item.Item2;
            neighbour.visited = true;
            RemoveRoomWall(r.Index.x, r.Index.y, item.Item1);
            
            stack.Push(neighbour);
        }
        else
        {
            stack.Pop();
        }
        return false;
    }
    private void CreateMaze()
    {
        if (generating) return;
        Reset();
        
        //Start & End
        RemoveRoomWall(0,0, Room.Directions.Bottom);
        RemoveRoomWall(_numX -1 , _numY -1, Room.Directions.Right);
        
        stack.Push(rooms[0,0]);

        GenerateMaze();
    }
    private void GenerateMaze()
    {
        generating = true;
        bool flag = false;

        while (!flag)
        {
            flag = GenerateStep();
        }

        generating = false;
    }
    private void Reset()
    {
        for (int i = 0; i < _numX; i++)
        {
            for (int j = 0; j < _numY; j++)
            {
                rooms[i,j].SetDirFlag(Room.Directions.Top, true);
                rooms[i,j].SetDirFlag(Room.Directions.Right, true);
                rooms[i,j].SetDirFlag(Room.Directions.Bottom, true);
                rooms[i,j].SetDirFlag(Room.Directions.Left, true);
                rooms[i, j].visited = false;
            }   
        }
    }
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

}

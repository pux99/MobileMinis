using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class MazeFactory : MonoBehaviour, I_GrafoTDA
{
    [SerializeField] private GameObject roomPrefab;
    Camera _cam;

    
    //The grid
    public Room[,] rooms = null;
    
    //Size
    private int _numX = 10;
    private int _numY = 10;
    
    //Each RoomSize
    [HideInInspector]public float roomSize;
    [HideInInspector]public float scaleFactor;
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
    
    //STARTS!
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
        InicializarGrafo();
        rooms = new Room[_numX, _numY];

        for (int i = 0; i < _numX; i++)
        {
            for (int j = 0; j < _numY; j++)
            {
                GameObject room = Instantiate(roomPrefab, this.transform, true);
                room.transform.localScale *= scaleFactor;
                room.transform.position = new Vector3( (i * roomSize) + xOffset, (j * roomSize) - yOffset, 0.0f);
                
                //room.name = "Room_" + i.ToString() + "_" + j.ToString();
                rooms[i, j] = room.GetComponent<Room>();
                rooms[i, j].posOnGrid = new Vector2Int(i, j);
                
                //Grafo agrego un vertice por cada Room.
                AgregarVertice(Vect2Vert(rooms[i, j].posOnGrid));
                room.name = Vect2Vert(rooms[i, j].posOnGrid).ToString();
            }   
        } 
        yield return null;
    }
    private void GetRoomSize()
    {
        float screenWidthWorld = _cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, _cam.nearClipPlane)).x - _cam.ScreenToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane)).x;
        float screenHeightWorld = (_cam.ScreenToWorldPoint(new Vector3(0, Screen.height, _cam.nearClipPlane)).y - _cam.ScreenToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane)).y) *.6f;
        
        float targetRoomWidth = screenWidthWorld / _numX;
        float targetRoomHeight = screenHeightWorld / _numY;
        float targetRoomSize = Mathf.Min(targetRoomWidth, targetRoomHeight);
        
        float currentRoomSize = roomPrefab.transform.Find("Floor").GetComponent<SpriteRenderer>().bounds.size.x;
        
        scaleFactor = targetRoomSize / currentRoomSize;
        
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
        ResetMaze();
        
        stack.Push(rooms[0,0]);

        while (stack.Count > 0)
        {
            GenerateStep();
            yield return null;
        }

        _generating = false;
    }
    private void RemoveRoomWall(Vector2Int v, Room.Directions dir)
    {
        if (dir != Room.Directions.None)
        {
            rooms[v.x,v.y].SetDirFlag(dir, false);
        }

        Room.Directions opp = Room.Directions.None;
        switch (dir)
        {
            case Room.Directions.Top:
                if (v.y < _numY -1)
                {
                    opp = Room.Directions.Bottom;
                    ++v.y;
                }

                break;
            case Room.Directions.Right:
                if (v.x < _numX - 1)
                {
                    opp = Room.Directions.Left;
                    ++v.x;
                }

                break;
            case Room.Directions.Bottom:
                if (v.y > 0)
                {
                    opp = Room.Directions.Top;
                    --v.y;
                }

                break;
            case Room.Directions.Left:
                if (v.x > 0)
                {
                    opp = Room.Directions.Right;
                    --v.x;
                }

                break;
        }
        if (opp != Room.Directions.None)
        {
            rooms[v.x,v.y].SetDirFlag(opp,false);
        }
        
    }
    private void GenerateStep()
    {
        if (stack.Count == 0) return;

        Room currentRoom = stack.Peek();
        var neighbours = GetNeighboursNotVisited(currentRoom.posOnGrid.x, currentRoom.posOnGrid.y);

        if (neighbours.Count > 0)
        {
            var nextRoomInfo = neighbours[UnityEngine.Random.Range(0, neighbours.Count)];
            Room nextRoom = nextRoomInfo.Item2;
            
            //Grafo: Agrego una arista de peso 1 para ambos vertices.
            if (!ExisteArista(Vect2Vert(currentRoom.posOnGrid), Vect2Vert(nextRoom.posOnGrid)))
            {
                AgregarArista(Vect2Vert(currentRoom.posOnGrid), Vect2Vert(nextRoom.posOnGrid), 1);
                AgregarArista(Vect2Vert(nextRoom.posOnGrid), Vect2Vert(currentRoom.posOnGrid), 1);
            }
            
            RemoveRoomWall(currentRoom.posOnGrid, nextRoomInfo.Item1);

            
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
    public void ResetMaze()
    {
        if (rooms == null || MAdy == null) return;
        foreach (var room in rooms)
        {
            room.visited = false;
            room.SetDirFlag(Room.Directions.Top, true);
            room.SetDirFlag(Room.Directions.Right, true);
            room.SetDirFlag(Room.Directions.Bottom, true);
            room.SetDirFlag(Room.Directions.Left, true);
        }
        for (int i = 0; i < cantNodos; i++)
        {
            for (int j = 0; j < cantNodos; j++)
            {
                MAdy[i, j] = 0;
            }
        }
    }
    public void SetFinnishLine()
    {
        rooms[_numX - 1, _numY - 1].SetFinnishLine();
    }
    
    //Grafos
    public int[,] MAdy;
    public int[] etiqs;
    public int cantNodos;
    public void InicializarGrafo()
    {
        //En vez de usar n lo hacemos dinamicamente con la cntidad total de cuartos.
        int totalRooms = _numX * _numY; 
        
        MAdy = new int[totalRooms, totalRooms];
        etiqs = new int[totalRooms];
        cantNodos = 0;
    }
    public void AgregarVertice(int v)
    {
        etiqs[cantNodos] = v;
        for (int i = 0; i <= cantNodos; i++)
        {
            MAdy[cantNodos, i] = 0;
            MAdy[i, cantNodos] = 0;
        }
        cantNodos++;
    }
    public void EliminarVertice(int v)
    {
        int ind = Vert2Indice(v);
 
        for (int k = 0; k < cantNodos; k++)
        {
            MAdy[k, ind] = MAdy[k, cantNodos - 1];
        }
 
        for (int k = 0; k < cantNodos; k++)
        {
            MAdy[ind, k] = MAdy[cantNodos - 1, k];
        }
 
        etiqs[ind] = etiqs[cantNodos - 1];
        cantNodos--;
    }
    private int Vert2Indice(int v)
    {
        int i = cantNodos - 1;
        while (i >= 0 && etiqs[i] != v)
        {
            i--;
        }
 
        return i;
    }
    public void AgregarArista(int v1, int v2, int peso)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        MAdy[o, d] = peso;
    }
    public void EliminarArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        MAdy[o, d] = 0;
    }
    public bool ExisteArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        return MAdy[o, d] != 0;
    }
    
    //Convierto al Vector del cuarto en un Vertice
    public int Vect2Vert(Vector2Int n)
    {
        return n.x * _numY + n.y;
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GenerateMaze : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;
    
    //Size?
    [SerializeField] private int height;
    [SerializeField] private int width;
    
    //The grid
    private Room[,] rooms = null;
    
    //Each room size
    private float roomWidth;
    private float roomHeight;
    
    //Stack for backtracking
    private Stack<Room> stack = new Stack<Room>();

    private bool generating = false;

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

    private void Start()
    {
        //GetRoomSize();
        rooms = new Room[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject room = Instantiate(roomPrefab, this.transform, true);
                room.transform.position = new Vector3(this.transform.position.x + (i*7), this.transform.position.x + (j*7), 0);
                room.name = "Room_" + i.ToString() + "_" + j.ToString();
                rooms[i, j] = room.GetComponent<Room>();
            }   
        }
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
                if (y < height -1)
                {
                    opp = Room.Directions.Bottom;
                    ++y;
                }

                break;
            case Room.Directions.Right:
                if (x < width -1)
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

    public List<Tuple<Room.Directions, Room>> GetNeighboursNotVisited(int cx, int cy)
    {
        List<Tuple<Room.Directions, Room>> neighbours = new List<Tuple<Room.Directions, Room>>();

        foreach (Room.Directions dir in Enum.GetValues(typeof(Room.Directions)))
        {
            int x = cx;
            int y = cy;

            switch (dir)
            {
                case Room.Directions.Top:
                    if (y < height -1)
                    {
                        ++y;
                        if (!rooms[x,y].visited)
                        {
                         neighbours.Add(new Tuple<Room.Directions, Room>(Room.Directions.Top, rooms[x,y]));   
                        }
                    }

                    break;
                case Room.Directions.Right:
                    if (x < width -1)
                    {
                        ++x;
                        if (!rooms[x,y].visited)
                        {
                            neighbours.Add(new Tuple<Room.Directions, Room>(Room.Directions.Right, rooms[x,y]));   
                        }
                    }

                    break;
                case Room.Directions.Bottom:
                    if (y > height)
                    {
                        --y;
                        if (!rooms[x,y].visited)
                        {
                            neighbours.Add(new Tuple<Room.Directions, Room>(Room.Directions.Bottom, rooms[x,y]));   
                        }
                    }

                    break;
                case Room.Directions.Left:
                    if (x > width)
                    {
                        --x;
                        if (!rooms[x,y].visited)
                        {
                            neighbours.Add(new Tuple<Room.Directions, Room>(Room.Directions.Left, rooms[x,y]));   
                        }
                    }

                    break;
            }
        }return neighbours;
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

    public void CreateMaze()
    {
        if (generating) return;
        Reset();
        
        RemoveRoomWall(0,0, Room.Directions.Bottom); //Inicio
        RemoveRoomWall(width -1 , height -1, Room.Directions.Right);
        
        stack.Push(rooms[0,0]);

        StartCoroutine(Corutine_Generate());
    }

    IEnumerator Corutine_Generate()
    {
        generating = true;
        bool flag = false;

        while (!flag)
        {
            flag = GenerateStep();
            yield return new WaitForSeconds(0.05f) ;
        }

        generating = false;
    }

    private void Reset()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                rooms[i,j].SetDirFlag(Room.Directions.Top, true);
                rooms[i,j].SetDirFlag(Room.Directions.Right, true);
                rooms[i,j].SetDirFlag(Room.Directions.Bottom, true);
                rooms[i,j].SetDirFlag(Room.Directions.Left, true);
                rooms[i, j].visited = false;
            }   
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!generating)
            {
                CreateMaze();
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum Directions
    {
        Top,
        Right,
        Bottom,
        Left,
        None,
    }

    [SerializeField] GameObject topWall;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject floor;

    private Dictionary<Directions, GameObject> walls = new Dictionary<Directions, GameObject>();
    
    public Vector2Int Index { get; set; }
    public bool visited { get; set; } = false;

    private Dictionary<Directions, bool> dirflags = new Dictionary<Directions, bool>();

    private void Awake()
    {
        walls[Directions.Top] = topWall;
        walls[Directions.Right] = rightWall;
        walls[Directions.Bottom] = bottomWall;
        walls[Directions.Left] = leftWall;
        
        foreach (Directions dir in Enum.GetValues(typeof(Directions)))
        {
            dirflags[dir] = true;
        }
    }

    public void SetActive(Directions dir, bool flag)
    {
        walls[dir].SetActive(flag);
    }

    public void SetDirFlag(Directions dir, bool flag)
    {
        dirflags[dir] = flag;
        SetActive(dir, flag);
    }

    public bool GetDirFlag(Directions dir)
    {
        return dirflags[dir];
    }

    public void SetFinnishLine()
    {
        floor.GetComponent<SpriteRenderer>().color = Color.yellow;
        floor.GetComponent<BoxCollider2D>().enabled = true;
        floor.AddComponent<EndLine>();
    }
}

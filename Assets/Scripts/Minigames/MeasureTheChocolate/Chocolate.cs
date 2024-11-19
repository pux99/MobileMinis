using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chocolate : MonoBehaviour
{
    public int Columns { get; private set; }
    public int Rows { get; private set; }
    public int Size => Columns * Rows;
    public Color Color { get; private set; }
    
    public void Initialize(int columns, int rows, Color color)
    {
        Columns = columns;
        Rows = rows;
        Color = color;
        
        Image moldImage = GetComponent<Image>();
        if (moldImage != null)
        {
            moldImage.color = color;
        }
    }
}

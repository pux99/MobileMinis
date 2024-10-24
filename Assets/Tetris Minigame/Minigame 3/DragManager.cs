using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragManager : MonoBehaviour
{
    public static DragManager Instance { get; private set; }

    public Image DraggedImage { get; private set; }

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
    
    public void SetPieceInfo(Image image)
    {
        DraggedImage = image;
    }
    
    public void ClearPieceInfo()
    {
        DraggedImage = null;
    }
}

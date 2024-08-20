using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class GridMaker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GridHex;
    GameObject Grid;
    GameObject temp;
    GridManager gridManager;
    public Vector2 Size;
    void Start()
    {
        Grid = new GameObject("Grid");
        gridManager=Grid.AddComponent<GridManager>();
        for (int i = 0; i < Size.x; i++) 
        {
            gridManager.list.Add(new List<GridManager.Hex>());
            for (int j = 0; j < Size.y; j++)
            {
                if(i%2==0)
                    temp = Instantiate(GridHex,new Vector3((3/2f)*0.5f*i+ (0.1f * i), (math.sqrt(3)*0.5f)*j+(0.1f*j),0), Quaternion.identity, Grid.transform);
                else
                    temp = Instantiate(GridHex, new Vector3((3 / 2f) * 0.5f * i + (0.1f * i), (math.sqrt(3) * 0.5f) * j + (0.1f * j) - ((math.sqrt(3) * 0.5f)/2)-0.05f, 0), Quaternion.identity, Grid.transform);
                temp.gameObject.name=i.ToString()+","+j.ToString();
                gridManager.list[i].Add(new GridManager.Hex());
                gridManager.list[i][j].gameObject = temp;
                gridManager.list[i][j].hexManager = temp.GetComponent<HexManager>();
            }
        }
        gridManager.Inicialize.Invoke();

    }
    
}

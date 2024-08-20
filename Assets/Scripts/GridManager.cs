using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GridManager : MonoBehaviour
{
    public List<List<Hex>> list = new List<List<Hex>>();
    public class Hex
    {
        public GameObject gameObject;
        public HexManager hexManager;
    }

    HexManager[] adjacent = new HexManager[6];
    public UnityEvent Inicialize;
    HexManager voidHex;

    private void Awake()
    {
            Inicialize = new UnityEvent();

        Inicialize.AddListener(InicializeGrid);
        voidHex = GameObject.Find("VoidRune").GetComponent<HexManager>();
        
    }
    void InicializeGrid()
    {
        
        for (int i = 0; i < list.Count; i++)
        {
            for(int j = 0; j < list[i].Count; j++) 
            {
                if (j % 2 != 0)
                {
                    if(i>0)
                        adjacent[0] = list[i - 1][j].hexManager;
                    else
                        adjacent[0] = voidHex;
                    if(j< list[i].Count-1)
                        adjacent[1] = list[i][j + 1].hexManager;
                    else
                        adjacent[1] = voidHex;
                    if (i < list.Count - 1 && j < list[i].Count-1)
                        adjacent[2] = list[i + 1][j + 1].hexManager;
                    else
                        adjacent[2] = voidHex;
                    if (i < list.Count - 1)
                        adjacent[3] = list[i + 1][j].hexManager;
                    else
                        adjacent[3] = voidHex;
                    if (i < list.Count - 1 && j > 0)
                        adjacent[4] = list[i + 1][j - 1].hexManager;
                    else
                        adjacent[4] = voidHex;
                    if (j > 0)
                        adjacent[5] = list[i][j - 1].hexManager;
                    else
                        adjacent[5] = voidHex;
                }
                else
                {
                    if (i > 0)
                        adjacent[0] = list[i - 1][j].hexManager;
                    else
                        adjacent[0] = voidHex;
                    if (i>0 && j < list[i].Count - 1)
                        adjacent[1] = list[i - 1][j + 1].hexManager;
                    else
                        adjacent[1] = voidHex;
                    if (j < list[i].Count - 1)
                        adjacent[2] = list[i][j + 1].hexManager;
                    else
                        adjacent[2] = voidHex;
                    if (i < list.Count - 1)
                        adjacent[3] = list[i + 1][j].hexManager;
                    else
                        adjacent[3] = voidHex;
                    if (j > 0)
                        adjacent[4] = list[i][j - 1].hexManager;
                    else
                        adjacent[4] = voidHex;
                    if (i > 0 && j > 0)
                    adjacent[5] = list[i - 1][j - 1].hexManager;
                    else
                        adjacent[5] = voidHex;
                }
                Debug.Log(i + "," + j);
                list[i][j].hexManager.InicializeHex(adjacent);
            }
        }
    }
}

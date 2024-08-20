using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorterlist : MonoBehaviour
{
    [SerializeField]public List<Players> players = new List<Players>();
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) { 
        players.Sort();
            foreach(var p in players)
            {
                Debug.Log(p.name);
            }
        }
    }
}
[Serializable]public class Players : IComparable<Players>
{
    [SerializeField] private int BattlesWin;
    [SerializeField] private int BattlesLoss;
    [SerializeField] public string name;

    public Players(int battlesWin,int battlesLoss)
    {
        BattlesWin = battlesWin;
        BattlesLoss = battlesLoss;
    }

    public int CompareTo(Players other)
    {
        if (other != null)
        {
            if (BattlesWin > other.BattlesWin)
                return 1;
            else
            {
                if (BattlesWin == other.BattlesWin)
                {
                    if (BattlesLoss < other.BattlesLoss) return 1;
                }
                return 1;

            }
        }
        return 1;

    }
}

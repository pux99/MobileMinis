using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyRune : MonoBehaviour
{
    public List<int> list = new List<int>();
    public bool rotable;
    public Image ImageConection;
    public Image ImageRune;
    enum RuneTypes { Power, Goal, Conection }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getConections(List<int> TempList,Sprite sprite)
    {
        list = TempList;
        ImageConection.sprite = sprite;
    }
    public void getRotable(bool TempRotable)
    {
        rotable = TempRotable;
    }
    public void getRuneType(bool energiced, bool goal ,Color color)
    {
        ImageRune.color = color;
    }
}

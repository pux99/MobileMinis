using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceProperty : MonoBehaviour
{
    enum PropertyTypes { Rune,Conection }
    [SerializeField] PropertyTypes PropertyType;
    public List<int> Conections = new List<int>();
    enum RuneTypes {Power,Goal,Conection }
    [SerializeField] RuneTypes RuneType;
    public GameObject rune;
    CopyRune copyRune;
    public Image conection;
    public Color RuneColor;
    private void Start()
    {
        copyRune = rune.GetComponent<CopyRune>();
    }

    public void giveProperty()
    {
        bool energiced=false;
        bool goal=false;
        if (PropertyType == PropertyTypes.Conection)
        {
            copyRune.getConections(Conections,conection.sprite);
        }
        else
        {
            
            switch (RuneType)
            {
                case RuneTypes.Power:
                    energiced = true;
                    break; 
                case RuneTypes.Goal:
                    goal=true;
                    break; 
                case RuneTypes.Conection:
                    break;
            }
            copyRune.getRuneType(energiced, goal, RuneColor);
        }
    }
}

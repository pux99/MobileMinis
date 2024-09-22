using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ListOfColors", menuName = "Colors/List of Colors", order = 1)]
public class SO_GroupOfColors : ScriptableObject
{
    [SerializeField]private List<Color> _colors;
    public List<Color> Colors { get=> _colors; }
}

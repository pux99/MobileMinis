using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ListOfColors", menuName = "Colors/List of Colors", order = 1)]
public class SO_GroupOfColors : ScriptableObject
{
    [SerializeField]private List<Color> colors;
    public List<Color> Colors => colors;
}

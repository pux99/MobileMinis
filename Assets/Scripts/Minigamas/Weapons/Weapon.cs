using Effects;
using Minigamas.GeneralUse;
using UnityEngine;

public class Weapon : ScriptableObject
{
    [SerializeField] private GameObject minigamePrefab;
    [SerializeField] private Effect completingEffect;
    [SerializeField] private Effect losingEffect;
}

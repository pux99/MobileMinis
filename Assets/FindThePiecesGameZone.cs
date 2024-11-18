using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindThePiecesGameZone : MonoBehaviour
{
    [SerializeField]private Canvas minigameCanvas;

    public Canvas MinigameCanvas
    {
        get => minigameCanvas;
        set => minigameCanvas = value;
    }
}

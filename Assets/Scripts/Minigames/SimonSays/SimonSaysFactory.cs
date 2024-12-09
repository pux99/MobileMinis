using System.Collections;
using System.Collections.Generic;
using Minigames.Factory;
using Minigames.GeneralUse;
using UnityEngine;

public class SimonSaysFactory : MinigameFactory
{
    [SerializeField] private SimonSaysMinigameController minigameController;
    public override MinigameController CreateMinigame()
    {
        return Instantiate(minigameController.gameObject).GetComponent<MinigameController>();
    }
}


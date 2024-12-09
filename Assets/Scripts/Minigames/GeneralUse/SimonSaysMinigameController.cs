using System.Collections;
using System.Collections.Generic;
using Minigames.GeneralUse;
using UnityEngine;

public class SimonSaysMinigameController : MinigameController
{
    [SerializeField] private SimonSaysManager minigame;

    private int _dmgCounter;
       
    protected override void Start()
    {
        _dmgCounter = 1;
        //minigame.WinMinigame += WiningMinigame;
        //minigame.LostMinigame += LosingMinigame;
    }

    protected override void StartMinigame()
    {
        
    }
        
    protected override void WiningMinigame()
    {
        if(minigameWeapon.CompletingEffect!=null)
            minigameWeapon.CompletingEffect.ApplyEffect(battleManager.PlayerCombatManager.PlayerHealth,minigameWeapon.CompletingEffectValue * _dmgCounter);
        _dmgCounter = (_dmgCounter <= 8) ? _dmgCounter + 1 : 0;
    }
    protected override void LosingMinigame()
    {
        _dmgCounter = 1;
        if(minigameWeapon.LosingEffect!=null)
            minigameWeapon.LosingEffect.ApplyEffect(battleManager.PlayerCombatManager.PlayerHealth,minigameWeapon.LosingEffectValue);
    }
        
    public override void ChangeToOtherMinigame()
    {

    }//nothing in this minigame For now
        

    public override void ResetMinigame()
    {
        _dmgCounter = 1;
    }
 
    public override void FinishingMinigame()
    {
        ResetMinigame();
    }
}

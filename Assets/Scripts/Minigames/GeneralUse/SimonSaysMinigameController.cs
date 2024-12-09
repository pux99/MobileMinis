using System.Collections;
using System.Collections.Generic;
using Minigames.GeneralUse;
using UnityEngine;

public class SimonSaysMinigameController : MinigameController
{
    [SerializeField] private ColorSequenceManager minigame;
       
    protected override void Start()
    {
        //minigame.WinMinigame += WiningMinigame;
        //minigame.LostMinigame += LosingMinigame;
    }

    protected override void StartMinigame()
    {
        
    }
        
    protected override void WiningMinigame()
    {
        if(minigameWeapon.CompletingEffect!=null)
            minigameWeapon.CompletingEffect.ApplyEffect(battleManager.PlayerCombatManager.PlayerHealth,minigameWeapon.CompletingEffectValue);
    }
    protected override void LosingMinigame()
    {
        if(minigameWeapon.LosingEffect!=null)
            minigameWeapon.LosingEffect.ApplyEffect(battleManager.PlayerCombatManager.PlayerHealth,minigameWeapon.LosingEffectValue);
    }
        
    public override void ChangeToOtherMinigame()
    {

    }//nothing in this minigame For now
        

    public override void ResetMinigame()
    {
        
    }
 
    public override void FinishingMinigame()
    {
        ResetMinigame();
    }
}

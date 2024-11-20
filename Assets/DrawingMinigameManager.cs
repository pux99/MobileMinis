using System.Collections;
using System.Collections.Generic;
using Minigames.Drawing;
using Minigames.GeneralUse;
using UnityEngine;

public class DrawingMinigameManager : MinigameController
{
    [SerializeField] private paintable minigame;
       
    protected override void Start()
    {
        minigame.WinMinigame += WiningMinigame;
        minigame.ChangeDrawing();
    }

    protected override void StartMinigame()
    {
        
    }
        
    protected override void WiningMinigame()
    {
        if(minigameWeapon.CompletingEffect!=null)
            minigameWeapon.CompletingEffect.ApplyEffect(battleManager.EnemyManager.Health,minigameWeapon.CompletingEffectValue);
        ResetMinigame();
    }
    protected override void LosingMinigame()
    {
        if(minigameWeapon.LosingEffect!=null)
            minigameWeapon.LosingEffect.ApplyEffect(battleManager.PlayerCombatManager.PlayerHealth,minigameWeapon.LosingEffectValue);
        ResetMinigame();
    }
        
    public override void ChangeToOtherMinigame()
    {
        
    }//nothing in this minigame For now
        

    public override void ResetMinigame()
    {
        minigame.ChangeDrawing();
    }
 
    public override void FinishingMinigame()
    {
            
    }//nothing in this minigame For now
}

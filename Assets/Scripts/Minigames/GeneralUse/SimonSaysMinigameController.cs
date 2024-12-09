using System;
using System.Collections;
using System.Collections.Generic;
using Minigames.GeneralUse;
using Minigames.SimonSays;
using UnityEngine;

public class SimonSaysMinigameController : MinigameController
{
    [SerializeField] private SimonButtonManager minigame;

    private int _dmgCounter;
       
    protected override void Start()
    {
        _dmgCounter = 1;
        minigame.WinMinigame += WiningMinigame;
        minigame.LostMinigame += LosingMinigame;
    }

    protected override void StartMinigame()
    {
    }
        
    protected override void WiningMinigame()
    {
        if(minigameWeapon.CompletingEffect!=null)
            minigameWeapon.CompletingEffect.ApplyEffect(battleManager.EnemyManager.Health,minigameWeapon.CompletingEffectValue * _dmgCounter);
        _dmgCounter = (_dmgCounter <= 8) ? _dmgCounter + 1 : 0;
        Debug.Log("Damage " + minigameWeapon.CompletingEffectValue * _dmgCounter);
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
        
    }

    private void OnEnable()
    {
        minigame.StartMiniGame();
    }

    private void OnDisable()
    {
        minigame.ChangeMinigame();
    }
}

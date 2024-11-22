using System;
using System.Collections;
using ManagerScripts;
using Tetris_Minigame.Scripts.Tetris;
using UnityEngine;
using UnityEngine.Serialization;

namespace Minigames.GeneralUse
{
    public class MazeMinigameController : MinigameController
    {
        [SerializeField] private MazeManager minigame;
       
        protected override void Start()
        {
            minigame.WinMinigame += WiningMinigame;
            minigame.LostMinigame += LosingMinigame;
            StartMinigame();
        }

        protected override void StartMinigame()
        {
            minigame.StartMinigame();
        }
        
        protected override void WiningMinigame()
        {
            if(minigameWeapon.CompletingEffect!=null)
                minigameWeapon.CompletingEffect.ApplyEffect(battleManager.PlayerCombatManager.PlayerHealth,minigameWeapon.CompletingEffectValue);
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
            minigame.ResetMinigame();
        }
 
        public override void FinishingMinigame()
        {
            
        }//nothing in this minigame For now
    }
}

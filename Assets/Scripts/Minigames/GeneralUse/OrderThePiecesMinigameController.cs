using System;
using System.Collections;
using ManagerScripts;
using Minigames.Tetris.OrderDePieces;
using Tetris_Minigame.Scripts.Tetris;
using UnityEngine;
using UnityEngine.Serialization;

namespace Minigames.GeneralUse
{
    public class OrderThePiecesMinigameController : MinigameController
    {
        [SerializeField] private OtPManager minigame;
       
        protected override void Start()
        {
            minigame.WinMinigame += WiningMinigame;
            //minigame.LostMinigame += LosingMinigame;
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
            StartMinigame();
        }
        protected override void LosingMinigame()
        {
            if(minigameWeapon.LosingEffect!=null)
                minigameWeapon.LosingEffect.ApplyEffect(battleManager.PlayerCombatManager.PlayerHealth,minigameWeapon.LosingEffectValue);
            ResetMinigame();
            StartMinigame();
        }
        
        public override void ChangeToOtherMinigame()
        {
            //minigame.DisableMinigame();
        }//nothing in this minigame For now
        

        public override void ResetMinigame()
        {
            minigame.ResetMinigame();
        }
 
        public override void FinishingMinigame()
        {
            ResetMinigame();
        }
    }
}

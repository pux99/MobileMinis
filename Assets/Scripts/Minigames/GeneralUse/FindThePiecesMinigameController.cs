using System;
using ManagerScripts;
using Tetris_Minigame.Scripts.Tetris;
using UnityEngine;
using UnityEngine.Serialization;

namespace Minigames.GeneralUse
{
    public class FindThePiecesMinigameController : MinigameController
    {
        [SerializeField] private FindThePiecesMinigame minigame;
       
        private void Start()
        {
            minigame.WinMinigame += WiningMinigame;
            minigame.LossMinigame += LosingMinigame;
            minigame.StartGame();
        }

        protected override void StartMinigame()
        {
            minigame.StartGame();
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
                minigameWeapon.LosingEffect.ApplyEffect(battleManager.EnemyManager.Health,minigameWeapon.LosingEffectValue);
            ResetMinigame();
        }
        
        public override void ChangeToOtherMinigame()
        {
        
        }//nothing in this minigame For now
        

        public override void ResetMinigame()
        {
            minigame.StartOver();
        }
 
        public override void FinishingMinigame()
        {
            
        }//nothing in this minigame For now
    }
}

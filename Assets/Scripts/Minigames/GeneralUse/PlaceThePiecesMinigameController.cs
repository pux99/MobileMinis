using System;
using System.Collections;
using ManagerScripts;
using Tetris_Minigame.Scripts.Tetris;
using UnityEngine;
using UnityEngine.Serialization;

namespace Minigames.GeneralUse
{
    public class PlaceThePiecesMinigameController : MinigameController
    {
        [SerializeField] private Tetris.PlaceThePieces.PtPGameManager minigame;
       
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
                minigameWeapon.CompletingEffect.ApplyEffect(battleManager.PlayerCombatManager.PlayerHealth,minigame.effectNumber);
            ResetMinigame();
            StartMinigame();
        }
        protected override void LosingMinigame()
        {
            ResetMinigame();
            StartMinigame();
        }
        
        public override void ChangeToOtherMinigame()
        {
            minigame.DisableMinigame();
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

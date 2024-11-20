using System;
using System.Collections;
using ManagerScripts;
using Tetris_Minigame.Scripts.Tetris;
using UnityEngine;
using UnityEngine.Serialization;

namespace Minigames.GeneralUse
{
    public class MeasureTheChocolateMinigameController : MinigameController
    {
        [SerializeField] private Minigames.MeasureTheChocolate.MinigameManager minigame;
       
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
            //HACE EL EFECTO DEL ARMA
            ResetMinigame();
        }
        protected override void LosingMinigame()
        {
            ResetMinigame();
        }
        
        public override void ChangeToOtherMinigame()
        {
            //minigame.DisableMinigame();
            minigame.enabled = false;
        }//nothing in this minigame For now
        

        public override void ResetMinigame()
        {
            minigame.ResetMinigame();
            StartMinigame();
        }
 
        public override void FinishingMinigame()
        {
            ResetMinigame();
        }
    }
}

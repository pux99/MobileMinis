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
       
        private void Start()
        {
            minigame.WinMinigame += WiningMinigame;
            minigame.LostMinigame += LosingMinigame;
            StartMinigame();
        }

        protected override void StartMinigame()
        {
            StartCoroutine(minigame.InitializeMinigameSequence());
        }
        
        protected override void WiningMinigame()
        {
            ResetMinigame();
        }
        protected override void LosingMinigame()
        {
            ResetMinigame();
        }
        
        public override void ChangeToOtherMinigame()
        {
        
        }//nothing in this minigame For now
        

        public override void ResetMinigame()
        {
            minigame.ResetMinigame();
            StartMinigame();
        }
 
        public override void FinishingMinigame()
        {
            
        }//nothing in this minigame For now
    }
}

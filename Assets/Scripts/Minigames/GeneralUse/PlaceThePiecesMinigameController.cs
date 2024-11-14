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
            //HACE EL EFECTO DEL ARMA
            Debug.Log(minigame.effectNumber + " de escudo recibido");
            ResetMinigame();
        }
        protected override void LosingMinigame()
        {
            ResetMinigame();
        }
        
        public override void ChangeToOtherMinigame()
        {
            minigame.DisableMinigame();
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

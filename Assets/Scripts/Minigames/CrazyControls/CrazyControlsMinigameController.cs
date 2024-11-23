using Minigames.GeneralUse;
using UnityEngine;

namespace Minigames.CrazyControls
{
    public class CrazyControlsMinigameController : MinigameController
    {
        [SerializeField] private CrazyControlsMinigame minigame;
       
        protected override void Start()
        {
            minigame.WinMinigame += WiningMinigame;
            StartMinigame();
        }

        protected override void StartMinigame()
        {
            minigame.ChangeState(CrazyControlsMinigame.States.Initialize);
        }
        
        protected override void WiningMinigame()
        {
            if(minigameWeapon.CompletingEffect!=null)
                minigameWeapon.CompletingEffect.ApplyEffect(battleManager.EnemyManager.Health,
                    minigameWeapon.CompletingEffectValue+battleManager.PlayerCombatManager.GetPlayerAttack());
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
            minigame.ChangeState(CrazyControlsMinigame.States.Reset);
        }
 
        public override void FinishingMinigame()
        {
            
        }//nothing in this minigame For now
    }
}

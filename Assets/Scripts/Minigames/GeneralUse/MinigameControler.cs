using ManagerScripts;
using Player.Weapons;
using UnityEngine;

namespace Minigames.GeneralUse
{
    public abstract class MinigameController : MonoBehaviour
    {
        [HideInInspector] public BattleManager battleManager;
        [HideInInspector] public Weapon minigameWeapon;

        protected virtual void Start()
        {
            DungeonManager.Instance.StartNewCombat += StartMinigame;
            DungeonManager.Instance.WinTheCombat += FinishingMinigame;
            DungeonManager.Instance.LossTheDungeon += FinishingMinigame;
        }

        protected virtual void StartMinigame()
        {
        
        }
        protected virtual void WiningMinigame()
        {
        
        }
        
        protected virtual void LosingMinigame()
        {
            
        }

        public virtual void ChangeToOtherMinigame()
        {
        
        }

        public virtual void ResetMinigame()
        {
            
        }

        public virtual void FinishingMinigame()
        {
            ResetMinigame();
        }
    
    }
}

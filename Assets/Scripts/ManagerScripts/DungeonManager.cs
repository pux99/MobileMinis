using System;
using Enemies;
using UnityEngine;

namespace ManagerScripts
{
    public class DungeonManager : MonoBehaviour
    {
        public static DungeonManager Instance { get; private set; }
        [SerializeField] private BattleManager battleManager;
        [SerializeField] private ExploringManager exploringManager;

        
        public Action WinTheCombat;
        public Action WinDungeon;
        public Action LossTheDungeon;
        public Action<SoEnemy> StartNewCombatEnemy;
        public Action StartNewCombat;

        private void Awake() 
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }
        void Start()
        {
            battleManager.CombatWin += EndOfCombat;
            battleManager.CombatLoss += LosingCombat;
        }
        private void EndOfCombat()
        {
            WinTheCombat?.Invoke();
            Debug.Log("win!!!!!!");
        }
        private void LosingCombat()
        {
            LossTheDungeon?.Invoke();
            Debug.Log("Loss!!!!!!");
        }

        public void NextCombat(SoEnemy enemy)
        {
            StartNewCombatEnemy?.Invoke(enemy);
            StartNewCombat?.Invoke();
            battleManager.StartNewBattle(enemy);
        }
    }
}

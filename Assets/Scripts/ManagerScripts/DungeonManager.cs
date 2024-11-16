using System;
using Core;
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
            ServiceLocator.Instance.GetService<EventManager>().CombatEnd += EndOfCombat;
            ServiceLocator.Instance.GetService<EventManager>().CombatLoss += HandleLoseCombat;
        }
        private void EndOfCombat()
        {
            WinTheCombat?.Invoke();
            Debug.Log("win!!!!!!");
        }
        private void HandleLoseCombat()
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

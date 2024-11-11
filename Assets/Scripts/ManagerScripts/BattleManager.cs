using System;
using Enemies;
using Minigames.GeneralUse;
using Minigames.Weapons;
using UnityEngine;

namespace ManagerScripts
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private EnemyManager enemyManager;

        public PlayerManager PlayerManager => playerManager;
        public EnemyManager EnemyManager => enemyManager;

        [SerializeField] private Weapon attackWeapon;
        [SerializeField] private Weapon defenceWeapon;
        [SerializeField] private Weapon supportWeapon;

    
        [SerializeField] private Transform attackMinigameHolder;
        [SerializeField] private Transform defenceMinigameHolder;
        [SerializeField] private Transform supportMinigameHolder;
        
        private MinigameController _attackMiniGameController;
        private MinigameController _defenceMiniGameController;
        private MinigameController _supportMiniGameController;

        public Action CombatWin;
        public Action CombatLoss;

        private void Start()
        {
            InstanceMinigame(attackWeapon,attackMinigameHolder,_attackMiniGameController);
            InstanceMinigame(defenceWeapon,defenceMinigameHolder,_defenceMiniGameController);
            InstanceMinigame(supportWeapon,supportMinigameHolder,_supportMiniGameController);
        }

        private void InstanceMinigame(Weapon minigameWeapon,Transform parent,MinigameController controller)
        {
            GameObject minigame= Instantiate(minigameWeapon.MinigameController.gameObject,parent);
            controller = minigame.GetComponent<MinigameController>();
            controller.battleManager = this;
            controller.minigameWeapon = minigameWeapon;
        }

        public void EnemyDefeated()
        {
            CombatWin?.Invoke();
        }

        public void PLayerDefeated()
        {
            CombatLoss?.Invoke();
        }

        public void StartNewBattle(SoEnemy enemy)
        {
            enemyManager.SetUpEnemy(enemy);
            ResetMinigames();
        }

        private void ResetMinigames()
        {
            
        }
    }
}

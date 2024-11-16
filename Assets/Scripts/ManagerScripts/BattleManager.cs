using System;
using Core;
using Enemies;
using Minigames.GeneralUse;
using Minigames.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

namespace ManagerScripts
{
    public class BattleManager : MonoBehaviour
    {
        [FormerlySerializedAs("playerManager")] [SerializeField] private PlayerCombatManager playerCombatManager;
        [SerializeField] private EnemyManager enemyManager;

        public PlayerCombatManager PlayerCombatManager => playerCombatManager;
        public EnemyManager EnemyManager => enemyManager;
    
        [SerializeField] private Transform attackMinigameHolder;
        [SerializeField] private Transform defenceMinigameHolder;
        [SerializeField] private Transform supportMinigameHolder;
        
        private MinigameController _attackMiniGameController;
        private MinigameController _defenceMiniGameController;
        private MinigameController _supportMiniGameController;

        private bool _minigamesInstantiated=false;
        
        private void Start()
        {
            ServiceLocator.Instance.GetService<EventManager>().CombatEnd += HandlerCombatEnd;
        }

        private void GenerateMinigames()
        {
            _attackMiniGameController=InstanceMinigame(playerCombatManager.PlayerStatsAndWeapons.AttackWeapon,attackMinigameHolder);
            _defenceMiniGameController=InstanceMinigame(playerCombatManager.PlayerStatsAndWeapons.DefenceWeapon,defenceMinigameHolder);
            _supportMiniGameController=InstanceMinigame(playerCombatManager.PlayerStatsAndWeapons.SupportWeapon,supportMinigameHolder);
            _minigamesInstantiated = true;
        }

        private MinigameController InstanceMinigame(Weapon minigameWeapon,Transform parent)
        {
            GameObject minigame= Instantiate(minigameWeapon.MinigameController.gameObject,parent);
            MinigameController controllerA = minigame.GetComponent<MinigameController>();
            controllerA.battleManager = this;
            controllerA.minigameWeapon = minigameWeapon;
            return controllerA;
        }
        
        [ContextMenu("tesEnemyDefeat")]
        public void EnemyDefeated()
        {
            ServiceLocator.Instance.GetService<EventManager>().OnCombatEnd();
        }

        public void PLayerDefeated()
        {
            ServiceLocator.Instance.GetService<EventManager>().OnCombatLoss();
        }

        public void StartNewBattle(SoEnemy enemy)
        {
            if (!_minigamesInstantiated)
                GenerateMinigames();
            enemyManager.SetUpEnemy(enemy);
            ResetMinigames();
            attackMinigameHolder.gameObject.SetActive(true);
            ServiceLocator.Instance.GetService<EventManager>().OnCombaStart();
        }

        private void ResetMinigames()
        {
            _attackMiniGameController.ResetMinigame();
            _defenceMiniGameController.ResetMinigame();
            _supportMiniGameController.ResetMinigame();
        }

        private void HandlerCombatEnd()
        {
            attackMinigameHolder.gameObject.SetActive(false);
            defenceMinigameHolder.gameObject.SetActive(false);
            supportMinigameHolder.gameObject.SetActive(false);
        }
    }
}

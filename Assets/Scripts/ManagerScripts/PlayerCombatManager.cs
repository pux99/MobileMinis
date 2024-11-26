using HealthSystem;
using Player;
using UI;
using UnityEngine;

namespace ManagerScripts
{
    public class PlayerCombatManager : MonoBehaviour
    {
        [SerializeField] private UHealth playerHealthManager ;
        [SerializeField] private UIHealth uiHealth;
        public UHealth PlayerHealth => playerHealthManager;
        [SerializeField] private SoPlayerStatsAndWeapons playerStatsAndWeapons;
        [SerializeField] private BattleManager battleManager;

        public SoPlayerStatsAndWeapons PlayerStatsAndWeapons
        {
            get => playerStatsAndWeapons;
            set => playerStatsAndWeapons = value;
        }

        private void Start()
        {
            playerHealthManager.OnDead += Defeated;
            playerStatsAndWeapons = SoPlayerStatsAndWeapons.Instance;
        }

        private void Defeated()
        {
            battleManager.PLayerDefeated();
        }

        public int GetPlayerAttack()
        {
            return playerStatsAndWeapons.Attack;
        }
    }
}

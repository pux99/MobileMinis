using Core;
using ManagerScripts;
using UnityEngine;

namespace UI
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private UIHealth playerUIHealth;
        [SerializeField] private PlayerCombatManager playerCombatManager;
        public PlayerCombatManager PlayerCombatManager => playerCombatManager;
        private void Start()
        {
            playerCombatManager.PlayerHealth.OnLifeChange += playerUIHealth.LifeChange;
            playerCombatManager.PlayerHealth.OnLifeChange += HandleLifeChange;
            playerCombatManager.PlayerHealth.OnShieldStateChange += playerUIHealth.ShieldChange;
            ServiceLocator.Instance.GetService<EventManager>().PlayerAttack += HandlePlayerAttack;
            ServiceLocator.Instance.GetService<EventManager>().PlayerDefend += HandlePlayerDefend;
            ServiceLocator.Instance.GetService<EventManager>().PlayerSupport += HandlePlayerSupport;
        }

        private void HandlePlayerAttack()
        {
            playerAnimator.SetTrigger("attack");
        }
        private void HandlePlayerDefend()
        {
            playerAnimator.SetTrigger("defence");
        }
        private void HandlePlayerSupport()
        {
            playerAnimator.SetTrigger("support");
        }
        private void HandleLifeChange(int newLife,int oldLife)
        {
            if(newLife<oldLife)
                playerAnimator.SetTrigger("damaged");
        }
    }
}

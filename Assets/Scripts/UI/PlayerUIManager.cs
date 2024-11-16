using ManagerScripts;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private UIHealth playerUIHealth;
    [FormerlySerializedAs("playerManager")] [SerializeField] private PlayerCombatManager playerCombatManager;
    public PlayerCombatManager PlayerCombatManager => playerCombatManager;
    private void Start()
    {
        playerCombatManager.PlayerHealth.OnLifeChange += playerUIHealth.LifeChange;
        playerCombatManager.PlayerHealth.OnShieldStateChange += playerUIHealth.ShieldChange;
    }
}

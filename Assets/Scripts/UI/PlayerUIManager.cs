using ManagerScripts;
using UI;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private UIHealth playerUIHealth;
    [SerializeField] private PlayerManager playerManager;
    public PlayerManager PlayerManager => playerManager;
    private void Start()
    {
        playerManager.PlayerHealth.OnLifeChange += playerUIHealth.LifeChange;
        playerManager.PlayerHealth.OnShieldStateChange += playerUIHealth.ShieldChange;
    }
}

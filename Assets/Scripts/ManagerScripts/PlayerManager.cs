using HealthSystem;
using UI;
using UnityEngine;

namespace ManagerScripts
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private UHealth playerHealthManager ;
        [SerializeField] private UIHealth uiHealth;
        public UHealth PlayerHealth => playerHealthManager;
    }
}

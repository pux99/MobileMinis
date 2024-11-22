using Core;
using ManagerScripts;
using UnityEngine;

namespace Command
{
    [CreateAssetMenu(menuName = "Command/GetShield", fileName = "Command/GetShield")]
    public class GetShield : SoCommand
    {
        public override void Execute()
        {
            Debug.LogWarning("This command needs an argument");
        }

        public override void Execute(string[] args)
        {
            switch (args[0])
            {
                case "Player":
                    ServiceLocator.Instance.GetService<BattleManager>().PlayerCombatManager.PlayerHealth.Shield();
                    break;
                case "Enemy":
                    ServiceLocator.Instance.GetService<BattleManager>().EnemyManager.Health.Shield();
                    break;
                default:
                    Debug.LogWarning("The first argument of this command Should be Player or Enemy");
                    break;
            }
        }
    }
}
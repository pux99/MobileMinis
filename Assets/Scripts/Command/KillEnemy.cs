using Core;
using ManagerScripts;
using UnityEngine;

namespace Command
{
    [CreateAssetMenu(menuName = "Command/KillEnemy", fileName = "Command/KillEnemy")]
    public class KillEnemy : SoCommand
    {
        public override void Execute()
        {
            ServiceLocator.Instance.GetService<BattleManager>().EnemyManager.Health.TakeDamage(9999);
        }

        public override void Execute(string[] args)
        {
            ServiceLocator.Instance.GetService<BattleManager>().EnemyManager.Health.TakeDamage(9999);
        }
    }
}
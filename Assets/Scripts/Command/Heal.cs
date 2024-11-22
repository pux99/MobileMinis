using Core;
using ManagerScripts;
using UnityEngine;

namespace Command
{
    [CreateAssetMenu(menuName = "Command/Heal", fileName = "Command/Heal")]
    public class Heal : SoCommand
    {
        public override void Execute()
        {
            Debug.LogWarning("This command needs an argument");
        }

        public override void Execute(string[] args)
        {
            int amount;
            switch (args[0])
            {
                case "Player":
                    if (int.TryParse(args[1],out amount))
                        ServiceLocator.Instance.GetService<BattleManager>().PlayerCombatManager.PlayerHealth.Heal(amount);
                    else
                        Debug.LogWarning("The second argument of this command should be a whole number");
                    break;
                case "Enemy":
                    if (int.TryParse(args[1],out amount))
                        ServiceLocator.Instance.GetService<BattleManager>().EnemyManager.Health.Heal(amount);
                    else
                        Debug.LogWarning("The second argument of this command should be a whole number");
                    break;
                default:
                    Debug.LogWarning("The first argument of this command Should be Player or Enemy");
                    break;
            }
        }
    }
}
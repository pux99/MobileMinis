using Core;
using UI;
using UnityEngine;

namespace Command
{
    [CreateAssetMenu(menuName = "Command/EnemyAttack", fileName = "Command/EnemyAttack")]

    public class EnemyAttack : SoCommand
    {
        public override void Execute()
        {
            ServiceLocator.Instance.GetService<CountDown>().CountdownEndEvent();
        }

        public override void Execute(string[] args)
        {
            ServiceLocator.Instance.GetService<CountDown>().CountdownEndEvent();
        }
    }
}
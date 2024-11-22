using Core;
using Player;
using UnityEngine;

namespace Command
{
    [CreateAssetMenu(menuName = "Command/SetAttack", fileName = "Command/SetAttack", order = 0)]
    public class SetAttack : SoCommand
    {
        public override void Execute()
        {
            Debug.LogWarning("This command needs an argument");
        }

        public override void Execute(string[] args)
        {
            if(int.TryParse(args[0],out int attack))
                ServiceLocator.Instance.GetService<SoPlayerStatsAndWeapons>().SetAttack(attack);
        }
    }
}
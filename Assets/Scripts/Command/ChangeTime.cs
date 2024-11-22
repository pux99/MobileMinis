using Core;
using UI;
using UnityEngine;

namespace Command
{    
    [CreateAssetMenu(menuName = "Command/ChangeTime", fileName = "Command/ChangeTime")]
    public class ChangeTime : SoCommand
    {
        public override void Execute()
        {
            Debug.LogWarning("This command needs an argument");
        }

        public override void Execute(string[] args)
        {
            
            if(float.TryParse(args[0],out float time))
                ServiceLocator.Instance.GetService<CountDown>().AddTime(time);
        }
    }
}
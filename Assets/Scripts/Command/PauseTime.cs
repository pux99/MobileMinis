using Core;
using UI;
using UnityEngine;

namespace Command
{
    [CreateAssetMenu(menuName = "Command/PauseTime", fileName = "Command/PauseTime")]
    public class PauseTime : SoCommand
    {
        public override void Execute()
        {
            Execute(new string[]{});
        }

        public override void Execute(string[] args)
        {
            if (ServiceLocator.Instance.GetService<CountDown>().IsPaused)
                ServiceLocator.Instance.GetService<CountDown>().ResumeTime();
            else
                ServiceLocator.Instance.GetService<CountDown>().PauseTime();
        }
    }
}
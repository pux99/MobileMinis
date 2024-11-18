using Core;
using HealthSystem;
using ManagerScripts;

namespace Effects
{
    public class ShieldUp : IEffect
    {
        public override void ApplyEffect(UHealth receiver,float value)
        {
            receiver.Shield();
            ServiceLocator.Instance.GetService<EventManager>().OnPlayerDefend();
        }
    }
}
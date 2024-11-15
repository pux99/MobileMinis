using HealthSystem;

namespace Effects
{
    public class ShieldUp : IEffect
    {
        public override void ApplyEffect(UHealth receiver,float value)
        {
            receiver.Shield();
        }
    }
}
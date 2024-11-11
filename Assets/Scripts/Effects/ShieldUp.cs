using HealthSystem;

namespace Effects
{
    public class ShieldUp : Effect
    {
        public override void ApplyEffect(UHealth receiver,float value)
        {
            receiver.Shield();
        }
    }
}
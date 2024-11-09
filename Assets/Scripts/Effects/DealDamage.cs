using HealthSystem;
using UnityEngine;

namespace Effects
{
    public class DealDamage : Effect
    {
        [SerializeField] private int damage;
        public override void ApplyEffect(UHealth receiver)
        {
            receiver.TakeDamage(damage);
        }
    }
}

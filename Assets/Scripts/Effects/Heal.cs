using HealthSystem;
using UnityEngine;

namespace Effects
{
    public class Heal : Effect
    {
        [SerializeField] private int heal;

        public override void ApplyEffect(UHealth receiver)
        {
            receiver.Heal(heal);
        }
    }
}
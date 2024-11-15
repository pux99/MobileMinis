using HealthSystem;
using UnityEngine;
using Effects;
namespace Effects
{
    public class Heal : IEffect
    {
        [SerializeField] private int heal;
        public override void ApplyEffect(UHealth receiver, float value)
        {
            receiver.Heal((int)value);
        }
    }
}
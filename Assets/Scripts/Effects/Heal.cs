using Core;
using HealthSystem;
using UnityEngine;
using Effects;
using ManagerScripts;

namespace Effects
{
    public class Heal : IEffect
    {
        [SerializeField] private int heal;
        public override void ApplyEffect(UHealth receiver, float value)
        {
            receiver.Heal((int)value);
            ServiceLocator.Instance.GetService<EventManager>().OnPlayerSupport();
        }
    }
}
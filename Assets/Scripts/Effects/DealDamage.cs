using Core;
using HealthSystem;
using ManagerScripts;
using UnityEngine;
using UnityEngine.Rendering;

namespace Effects
{
    public class DealDamage : IEffect
    {
        [SerializeField] private int damage;
        public override void ApplyEffect(UHealth receiver,float value)
        {
            receiver.TakeDamage((int)value);
            ServiceLocator.Instance.GetService<EventManager>().OnPlayerAttack();
        }
    }
}

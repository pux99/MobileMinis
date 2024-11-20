using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace HealthSystem
{
    public class UHealth : MonoBehaviour
    {
        [SerializeField] private int maxHp;
        [FormerlySerializedAs("StartingHp")] [SerializeField] private int startingHp;
        private Health _health ;
        public Action OnDead;
        public Action<int,int> OnLifeChange;
        public Action<bool> OnShieldStateChange;

        private void Awake()
        {
            _health = new Health(maxHp,startingHp);
            _health.OnHeal +=LifeChange ;
            _health.OnDamage += LifeChange;
            _health.OnDeath += Death;
            _health.OnShieldBreak += ShieldBreak;
            _health.OnSetShielded += ShieldReform;
        }

        public void Heal(int heal)
        {
            _health.Heal(heal);
        }
        public void FullHeal()
        {
            _health.Heal(maxHp);
        }

        public void Shield()
        {
            _health.ShieldUp();
        }

        public void TakeDamage(int damage)
        {
            _health.TakeDamage(damage);
        }
        private void LifeChange(int oldHp, int newHp)
        {
            OnLifeChange?.Invoke(_health.MaxHp,newHp);
        }

        private void ShieldBreak()
        {
            OnShieldStateChange?.Invoke(false);
        }
        private void ShieldReform()
        {
            OnShieldStateChange?.Invoke(true);
        }
        void Death()
        {
            OnDead?.Invoke();
        }

        public void SetMaxHp(int MaxHP)
        {
            _health.MaxHp = MaxHP;
        }
    }
}

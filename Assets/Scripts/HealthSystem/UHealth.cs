using System;
using UnityEngine;

namespace HealthSystem
{
    public class UHealth : MonoBehaviour
    {
        [SerializeField] private int maxHp;
        [SerializeField] private int StartingHp;
        private Health _health ;
        public Action OnDead;
        public Action<int,int> OnLifeChange;
        public Action<bool> OnShieldStateChange;

        private void Awake()
        {
            _health = new Health(maxHp,StartingHp);
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
            OnLifeChange?.Invoke(maxHp,newHp);
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
    }
}

using System;

namespace HealthSystem
{
    [Serializable]
    public class Health
    {
        public int MaxHp;
        public int Hp;
        public bool Shield;
        public Action OnDeath;
        public Action<int, int> OnDamage;
        public Action<int, int> OnHeal;
        public Action OnShieldBreak;
        public Action OnSetShielded;
        
        public Health(int maxHp, int hp)
        {
            MaxHp = maxHp;
            Hp = hp;
        }

        public void Heal(int heal)
        {
            if (heal > 0)
            {
                int oldValue = Hp;
                Hp += heal;
                if (Hp > MaxHp)
                    Hp = MaxHp;
                OnHeal.Invoke(oldValue, Hp);
            }
            else
            {
                Console.WriteLine("Heal do not accept negative numbers");
            }
        }
        public void ShieldUp()
        {
            if (!Shield)
            {
                Shield = true;
                OnSetShielded?.Invoke();
            }
            else
            {
                Console.WriteLine("You already have a shield");
            }
        }

        public void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                if (!Shield)
                {
                    int oldValue = Hp;
                    Hp -= damage;
                    OnDamage(oldValue,Hp);
                    if (Hp <= 0)
                        OnDeath?.Invoke();
                }
                else
                {
                    Shield = false;
                    OnShieldBreak?.Invoke();
                }
            }
            else
            {
                Console.WriteLine("Heal do not accept negative numbers");
            }
        }

    };
}

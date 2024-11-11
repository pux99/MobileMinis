using Effects;
using HealthSystem;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "NewEnemy",menuName = "")]
    public class SoEnemy : ScriptableObject
    {
        public Sprite enemyArt;
        public Effect enemyAttack;
        public int maxLife;
        public int damage;
        public float attackFrequency;

        public void Attack(UHealth target)
        {
            enemyAttack.ApplyEffect(target,damage);
        }
    }
}

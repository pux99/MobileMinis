using UnityEngine;

namespace AbstractFactory
{
    public abstract class CrazyEnemy :MonoBehaviour , IConfigurable<EnemyConfig>
    {
        public abstract void Move();
        public abstract void Attack();
        public abstract void Death();
        public abstract void SetUp(ICrazyWeapon weapon);
        public abstract GameObject GetGameObject();
        public abstract void Configure(EnemyConfig config);
    }
}
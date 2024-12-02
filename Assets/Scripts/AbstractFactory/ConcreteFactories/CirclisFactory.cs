using AbstractFactory.Enemies;
using UnityEngine;
using UnityEngine.Serialization;

namespace AbstractFactory.ConcreteFactories
{
    public class CirclisFactory :MonoBehaviour ,ICrazyEnemyFactory
    {
        [SerializeField]private Circlis prefab;
        public ICrazyEnemy CreateEnemy()
        {
            ICrazyEnemy enemy = Instantiate(prefab);
            enemy.SetUp(CreateWeapon());
            return enemy;
        }

        public ICrazyWeapon CreateWeapon()
        {
            return new CirclisStrick();
        }

        public bool CheckType<T>()
        {
            return typeof(T) == prefab.GetType();
        }
    }
}

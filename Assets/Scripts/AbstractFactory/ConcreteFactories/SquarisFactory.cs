using AbstractFactory.Enemies;
using UnityEngine;

namespace AbstractFactory.ConcreteFactories
{
    public class SquarisFactory : MonoBehaviour,ICrazyEnemyFactory
    {
        [SerializeField]private Squaris prefab;
        public ICrazyEnemy CreateEnemy()
        {
            ICrazyEnemy enemy = Instantiate(prefab);
            enemy.SetUp(CreateWeapon());
            return enemy;
        }

        public ICrazyWeapon CreateWeapon()
        {
            return (ICrazyWeapon)new SquarisStrick();
        }

        public bool CheckType<T>()
        {
            return typeof(T) == prefab.GetType();
        }
    }
}

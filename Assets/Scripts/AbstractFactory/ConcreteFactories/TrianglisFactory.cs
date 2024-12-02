using System;
using AbstractFactory.Enemies;
using UnityEngine;

namespace AbstractFactory.ConcreteFactories
{
    public class TrianglisFactory :MonoBehaviour,ICrazyEnemyFactory
    {
        [SerializeField]private Trianglis prefab;
        public ICrazyEnemy CreateEnemy()
        {
            ICrazyEnemy enemy = Instantiate(prefab);
            enemy.SetUp(CreateWeapon());
            return enemy;
        }

        public ICrazyWeapon CreateWeapon()
        {
            return (ICrazyWeapon)new TrianglisStrick();
        }

        public bool CheckType<T>()
        {
            return typeof(T) == prefab.GetType();
        }
    }
}


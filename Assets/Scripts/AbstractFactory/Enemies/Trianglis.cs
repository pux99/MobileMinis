using Core;
using ManagerScripts;
using UnityEngine;

namespace AbstractFactory.Enemies
{
    public class Trianglis : CrazyEnemy
    {
        private CrazyEnemy _crazyEnemyImplementation;
        private float _speed;
        private ICrazyWeapon _weapon;
        private void Start()
        {
            ServiceLocator.Instance.GetService<EventManager>().CrazyLevelChanger += Death;
        }
        public override void Move()
        {
            _crazyEnemyImplementation.Move();
        }

        public override void Attack()
        {
            _crazyEnemyImplementation.Attack();
        }

        public override void Death()
        {
            ServiceLocator.Instance.GetService<AbstractFactory.ConcreteFactories.EnemyManager>().ReturnEnemy(this);
            //_crazyEnemyImplementation.Death();
        }

        public override void SetUp(ICrazyWeapon weapon)
        {
            _weapon = weapon;
        }

        public override GameObject GetGameObject() => gameObject;

        public override void Configure(EnemyConfig config)
        {
            _speed = config.Speed;
        }
    }
}

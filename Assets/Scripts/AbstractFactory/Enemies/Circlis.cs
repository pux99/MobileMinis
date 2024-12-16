using System;
using Core;
using ManagerScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace AbstractFactory.Enemies
{
    public class Circlis : CrazyEnemy
    {
        private ICrazyWeapon _weapon;

        [SerializeField]private float _speed;
        [SerializeField]private float _radius;
        private float _tImeCounter;

        private void Start()
        {
            ServiceLocator.Instance.GetService<EventManager>().CrazyLevelChanger += Death;
        }

        private void Update()
        {
            Move();
        }

        public override void Move()
        {
            _tImeCounter += Time.deltaTime * _speed;

            float x = Mathf.Cos(_tImeCounter) * _radius;
            float y = Mathf.Sin(_tImeCounter) * _radius;

            transform.position = new Vector3(x, y, 0);
        }

        public override void Attack()
        {
            _weapon.Attack();
        }

        public override void Death()
        {
            ServiceLocator.Instance.GetService<AbstractFactory.ConcreteFactories.EnemyManager>().ReturnEnemy(this);
        }

        public override void SetUp(ICrazyWeapon weapon)
        {
            _weapon = weapon;
        }
        public override GameObject GetGameObject() => gameObject;
        private void OnCollisionEnter2D(Collision2D other)
        {
            _speed *= -1;
        }


        public override void Configure(EnemyConfig config)
        {
            _speed = config.Speed;
        }
    }
}

using System;
using UnityEngine;

namespace AbstractFactory.Enemies
{
    public class Circlis : MonoBehaviour ,ICrazyEnemy
    {
        private ICrazyWeapon _weapon;

        [SerializeField]private float _speed;
        [SerializeField]private float _radius;
        private float _tImeCounter;
        
        private void Update()
        {
            Move();
        }

        public void Move()
        {
            _tImeCounter += Time.deltaTime * _speed;

            float x = Mathf.Cos(_tImeCounter) * _radius;
            float y = Mathf.Sin(_tImeCounter) * _radius;

            transform.position = new Vector3(x, y, 0);
        }

        public void Attack()
        {
            _weapon.Attack();
        }

        public void Death()
        {
            throw new System.NotImplementedException();
        }

        public void SetUp(ICrazyWeapon weapon)
        {
            _weapon = weapon;
        }
        public GameObject GetGameObject() => gameObject;
        private void OnCollisionEnter2D(Collision2D other)
        {
            _speed *= -1;
        }

        
    }
}

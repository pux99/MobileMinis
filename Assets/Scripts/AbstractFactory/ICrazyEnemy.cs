using UnityEngine;

namespace AbstractFactory
{
    public interface ICrazyEnemy
    {
        void Move();
        void Attack();
        void Death();
        void SetUp(ICrazyWeapon weapon);
        GameObject GetGameObject();
    }
}

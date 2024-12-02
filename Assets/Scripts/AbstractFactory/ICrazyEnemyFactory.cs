using System;

namespace AbstractFactory
{
    public interface ICrazyEnemyFactory 
    {
        ICrazyEnemy CreateEnemy();
        ICrazyWeapon CreateWeapon();

        bool CheckType<T>();
    }
}

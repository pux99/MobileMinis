using System;

namespace AbstractFactory
{
    public interface ICrazyEnemyFactory 
    {
        CrazyEnemy CreateEnemy();
        ICrazyWeapon CreateWeapon();

        bool CheckType<T>();
    }
}

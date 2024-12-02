using System.Collections;
using System.Collections.Generic;
using AbstractFactory;
using UnityEngine;

public class Trianglis : MonoBehaviour ,ICrazyEnemy
{
    private ICrazyEnemy _crazyEnemyImplementation;
    private ICrazyWeapon _weapon;
    public void Move()
    {
        _crazyEnemyImplementation.Move();
    }

    public void Attack()
    {
        _crazyEnemyImplementation.Attack();
    }

    public void Death()
    {
        _crazyEnemyImplementation.Death();
    }

    public void SetUp(ICrazyWeapon weapon)
    {
        _weapon = weapon;
    }

    public GameObject GetGameObject() => gameObject;

}

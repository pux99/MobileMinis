using System.Collections;
using System.Collections.Generic;
using AbstractFactory;
using AbstractFactory.Enemies;
using Core;
using Pool;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<ICrazyEnemy> _enemies;
    ICrazyEnemy aas;
    void Start()
    {
        _enemies.Add(new Circlis());
        _enemies.Add(new Trianglis());
        _enemies.Add(new Squaris());

        ServiceLocator.Instance.GetService<CrazyEnemyPool>().GetElement<Circlis>();
    }

    private void SpawnEnemy()
    {
        
    }
}

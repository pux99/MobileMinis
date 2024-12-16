using System;
using System.Collections;
using System.Collections.Generic;
using AbstractFactory;
using Core;
using ManagerScripts;
using UnityEngine;
using UnityEngine.Serialization;

public class CrazySpawner : MonoBehaviour
{
    [SerializeField] private CrazyEnemy enemy;
    [SerializeField] private float speed;
    private CrazyEnemy currentEnemy;
    void Start()
    {
        currentEnemy = ServiceLocator.Instance.GetService<AbstractFactory.ConcreteFactories.EnemyManager>().GetEnemy(enemy.ToString(),new EnemyConfig(speed));
        currentEnemy.transform.parent = ServiceLocator.Instance
            .GetService<AbstractFactory.ConcreteFactories.EnemyManager>().transform;
        currentEnemy.transform.position = transform.position;
        currentEnemy.name = enemy.name;
    }
    
}

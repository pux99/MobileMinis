using System.Collections;
using System.Collections.Generic;
using AbstractFactory;
using AbstractFactory.ConcreteFactories;
using AbstractFactory.Enemies;
using Core;
using Pool;
using UnityEngine;

public class CrazyEnemyManager : MonoBehaviour
{
    private Factory<CrazyEnemy, EnemyConfig> _factory;
    private BasicPool<CrazyEnemy,EnemyConfig> _trianglisPool;
    public Trianglis TrianglisPrefab;
    private void Awake()
    {
        _factory = new Factory<CrazyEnemy,EnemyConfig>(TrianglisPrefab);
        _trianglisPool = new BasicPool<CrazyEnemy,EnemyConfig>(_factory.Create);
        ServiceLocator.Instance.RegisterService(this);
    }
}

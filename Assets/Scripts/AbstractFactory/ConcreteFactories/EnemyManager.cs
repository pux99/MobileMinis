using System.Collections.Generic;
using Core;
using UnityEngine;

namespace AbstractFactory.ConcreteFactories
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField]private List<CrazyEnemy> enemys;
        private Dictionary<string, Factory<CrazyEnemy, EnemyConfig>> _factories =new Dictionary<string, Factory<CrazyEnemy, EnemyConfig>>();
        private Dictionary<string, BasicPool<CrazyEnemy, EnemyConfig>> _pools =new Dictionary<string, BasicPool<CrazyEnemy, EnemyConfig>>();
        private void Awake()
        {
            foreach (var enemy in enemys)
            {
                _factories.Add(enemy.ToString(),new Factory<CrazyEnemy,EnemyConfig>(enemy));
                _pools.Add(enemy.ToString(),new BasicPool<CrazyEnemy,EnemyConfig>(_factories[enemy.ToString()].Create));
                Debug.Log(enemy.GetComponent<CrazyEnemy>().ToString());
            }
            ServiceLocator.Instance.RegisterService(this);
        }

        public CrazyEnemy GetEnemy(string enemyType,EnemyConfig config)
        {
            return _pools[enemyType].GetElement(config);
        }

        public void ReturnEnemy(CrazyEnemy enemy)
        {
            _pools[enemy.ToString()].ReturnElement(enemy);
        }
    }
}

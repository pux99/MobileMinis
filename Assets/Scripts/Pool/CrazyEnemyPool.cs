using System.Collections.Generic;
using AbstractFactory;
using AbstractFactory.Enemies;
using Core;
using UnityEngine;

namespace Pool
{
    public class CrazyEnemyPool :MonoBehaviour, IPool<ICrazyEnemy>
    {
        private List<ICrazyEnemy> _pool= new List<ICrazyEnemy>();
        [SerializeField] private CrazyEnemyFactory factory;

        private void Awake()
        {
            ServiceLocator.Instance.RegisterService(this);
        }
        private void Start()
        {
            GetElement<Trianglis>();
            GetElement<Circlis>();

            ReturnElement(GetElement<Squaris>());
            GetElement<Squaris>();
        }

        public ICrazyEnemy GetElement<T1>()
        {
            if (_pool.Count > 0)
            {
                ICrazyEnemy element =_pool.Find(x=>x.GetType()==typeof(T1));
                if (element != null)
                {
                    element.GetGameObject().SetActive(true);
                    _pool.Remove(element);
                    return element;
                }
            }
            return CreateElement<T1>();
        }

        private ICrazyEnemy CreateElement<T1>()
        {
            return factory.GetEnemy<T1>();
        }

        public void ReturnElement(ICrazyEnemy element)
        {
            element.GetGameObject().SetActive(false);
            _pool.Add(element);
        }
    }
}
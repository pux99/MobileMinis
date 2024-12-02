using System;
using System.Collections.Generic;
using AbstractFactory;
using AbstractFactory.Enemies;
using Core;
using UnityEngine;

namespace Pool
{
    public class EnemiesManager : MonoBehaviour
    {
        private Factory<Trianglis, Pool> _factory;
        private BasicPool<Trianglis> _trianglisPool;

        private void Awake()
        {
            _factory = new Factory<Trianglis,TConfig>();
            _trianglisPool = new BasicPool<Trianglis>(_factory.Create);
            ServiceLocator.Instance.RegisterService(this);
        }
    }
    public class BasicPool<T, TConfig> : IPool<T> where T : MonoBehaviour
    {
        private List<T> _pool= new();
        private readonly Func<T, Vector3, Quaternion, TConfig> _createElement;

        public BasicPool(Func<T, Vector3, Quaternion, TConfig> createElement)
        {
            _createElement = createElement;
        }

        public T GetElement<T1>()
        {
            if (_pool.Count > 0)
            {
                T element =_pool.Find(x=>x.GetType()==typeof(T1));
                if (element != null)
                {
                    element.gameObject.SetActive(true);
                    _pool.Remove(element);
                    return element;
                }
            }
            return _createElement();
        }

        public void ReturnElement(T element)
        {
            element.gameObject.SetActive(false);
            _pool.Add(element);
        }
    }
}
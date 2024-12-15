using System;
using System.Collections.Generic;
using Pool;
using Unity.Mathematics;
using UnityEngine;

namespace Core
{
    public class BasicPool<T, TConfig> : IPool<T, TConfig> where T : MonoBehaviour
    {
        private List<T> _pool= new();
        private readonly Func< TConfig,T> _createElement;
        public BasicPool(Func< TConfig,T> createElement)
        {
            _createElement = createElement;
        }
        public T GetElement( TConfig config)
        {
            if (_pool.Count > 0)
            {
                T element =_pool[0];
                if (element != null)
                {
                    element.gameObject.SetActive(true);
                    _pool.Remove(element);
                    return element;
                }
            }
            return _createElement(config);
        }
        public void ReturnElement(T element)
        {
            element.gameObject.SetActive(false);
            _pool.Add(element);
        }
        
    }
}

using System;
using Core;
using UnityEngine;

namespace ManagerScripts
{
    public class EventManager : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator serviceLocator = ServiceLocator.Instance;
            serviceLocator.RegisterService<EventManager>(this);
        }

        public Action CombatEnd;
        public void OnCombatEnd() => CombatEnd?.Invoke();
    }
}

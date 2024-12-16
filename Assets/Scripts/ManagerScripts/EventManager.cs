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
        public Action CrazyLevelChanger;
        public void OnCrazyLevelChanger() => CrazyLevelChanger?.Invoke();
        
        public Action CombatStart;
        public void OnCombaStart() => CombatStart?.Invoke();

        public Action CombatWin;
        public void OnCombatWin() => CombatWin?.Invoke();
        
        public Action DungeonWin;
        public void OnDungeonWin() => DungeonWin?.Invoke();
        
        public Action CombatLoss;
        public void OnCombatLoss() => CombatLoss?.Invoke();

        public Action Pause;
        public void OnPause() => Pause?.Invoke();

        public Action Resume;
        public void OnResume() => Resume?.Invoke();
        
        public Action PlayerAttack;
        public void OnPlayerAttack() => PlayerAttack?.Invoke();
        
        public Action PlayerDefend;
        public void OnPlayerDefend() => PlayerDefend?.Invoke();
        
        public Action PlayerSupport;
        public void OnPlayerSupport() => PlayerSupport?.Invoke();
        
        public Action LevelUp;
        public void OnLevelUP() => LevelUp?.Invoke();
        
        public Action ExploringUI;
        public void OnExploringUI() => ExploringUI?.Invoke();
    }
}

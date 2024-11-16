using Core;
using UnityEngine;

namespace ManagerScripts
{
    public class CurrentStateManager : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator serviceLocator = ServiceLocator.Instance;
            serviceLocator.RegisterService<CurrentStateManager>(this);
        }
        public enum State
        {
            InPause,
            InCombat,
            InExploration,
            InStartingMenu,
            InMainHub,
            InInventory,
            InWeaponSelector,
            InStartingState
        }

        private State _currentState = State.InStartingState;

        public State CurrentState()
        {
            return _currentState;
        }

        public void ChangeState(State newState)
        {
            _currentState = newState;
        }
    }
}

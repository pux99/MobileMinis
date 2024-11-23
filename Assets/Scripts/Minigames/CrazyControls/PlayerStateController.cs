using System;
using Minigames.CrazyControls.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

namespace Minigames.CrazyControls
{
    public class PlayerStateController : MonoBehaviour
    {
        [SerializeField] private string currentStateName;
        private IPlayerState _currentState;
        private CrazyMoveController _moveController;
        public Vector3 startingPos;

        private IPlayerState _pausedState;

        public CrazyMoveController MoveController => _moveController;

        public IdleState idleState = new IdleState();
        public MoveState moveState = new MoveState();
        public StunState stunState = new StunState();
        public ConfuseState confuseState = new ConfuseState();
        public PauseState PauseState = new PauseState();

        private void OnEnable()
        {
            _currentState = idleState;
        }

        private void Start()
        {
            _moveController = GetComponent<CrazyMoveController>();
            startingPos = transform.position;
        }

        private void Update()
        {
            _currentState = _currentState.Update(this);
            currentStateName = _currentState.ToString();
        }
        private void FixedUpdate()
        {
            _currentState = _currentState.FixUpdate(this);
            currentStateName = _currentState.ToString();
        }

        public void ChangeState(IPlayerState state)
        {
            switch (state)
            {
                case StunState :
                    _currentState = stunState;
                    break;
                case ConfuseState:
                    _currentState = confuseState;
                    break;
            }
            //_currentState = state; im not sure why this is not working
        }
        
        public void Pause()
        {
            _pausedState = _currentState;
            _currentState = PauseState;
        }
        public void Resume()
        {
            _currentState = _pausedState;
        }
    }
}

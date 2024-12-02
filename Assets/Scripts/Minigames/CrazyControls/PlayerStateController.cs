using System;
using System.Collections.Generic;
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
        private Dictionary<String, IState> _states;

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
            _currentState.Update(this);
            currentStateName = _currentState.ToString();
        }
        private void FixedUpdate()
        {
            _currentState.FixUpdate(this);
            currentStateName = _currentState.ToString();
        }

        public void ChangeState(IPlayerState state)
        {
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Move()
        {
            if(currentStateName==idleState.ToString())ChangeState(moveState);
        }

        public void Idle()
        {
            if(currentStateName==moveState.ToString())ChangeState(idleState);
        }
        
        public void Pause()
        {
            _pausedState = _currentState;
            ChangeState(PauseState);
        }
        public void Resume()
        {
            ChangeState(_pausedState);
        }
    }
}

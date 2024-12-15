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

        private readonly Dictionary<string, (IPlayerState state, List<IPlayerState> StatesTochange)> _stateDictionary
            =new Dictionary<string, (IPlayerState state, List<IPlayerState> StatesTochange)>();
        private IPlayerState _pausedState;
        public CrazyMoveController MoveController => _moveController;
        private Dictionary<String, IState> _states;

        public IdleState IdleState = new IdleState();
        public MoveState MoveState = new MoveState();
        public StunState StunState = new StunState();
        public PauseState PauseState = new PauseState();

        private void OnEnable()
        {
            _currentState = IdleState;
        }

        private void Start()
        {
            _moveController = GetComponent<CrazyMoveController>();
            MakeDictionary();
            startingPos = transform.position;
        }

        private void MakeDictionary()
        {
            _stateDictionary.Add(IdleState.ToString(),(IdleState,new List<IPlayerState>{MoveState,StunState,PauseState}));
            _stateDictionary.Add(MoveState.ToString(),(MoveState,new List<IPlayerState>{IdleState,StunState,PauseState}));
            _stateDictionary.Add(StunState.ToString(),(StunState,new List<IPlayerState>{IdleState,PauseState}));
            _stateDictionary.Add(PauseState.ToString(),(PauseState,new List<IPlayerState>{IdleState,MoveState,StunState}));
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
            foreach (var stateToChange in _stateDictionary[_currentState.ToString()].StatesTochange)
            {
                if (state.ToString() == stateToChange.ToString())
                {
                    _currentState.Exit();
                    _currentState = state;
                    _currentState.Enter();
                    return;
                }
            }
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

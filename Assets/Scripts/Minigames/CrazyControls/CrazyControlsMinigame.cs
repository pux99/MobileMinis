using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Minigames.CrazyControls
{
    public class CrazyControlsMinigame : MonoBehaviour
    {
        public enum States
        {
            Initialize,
            Game,
            Pause,
            Reset,
            Win,
            Quit,
            NoState
        }

        public Action WinMinigame;

        private States _currentState;

        [SerializeField]private PlayerStateController _player;
        [SerializeField] private GameObject game;
        [SerializeField] private CrazyMoveController crazyMoveController;
        

        [SerializeField] private List<GameObject> levels;
        private GameObject _currentLevel;
        private void Update()
        {
            switch (_currentState)
            {
                case States.Initialize:
                    InitializeState();
                    break;
                case States.Game:
                    GameState();
                    break;
                case States.Pause:
                    PauseState();
                    break;
                case States.Reset:
                    ResetState();
                    break;
                case States.Win:
                    WinState();
                    break;
                case States.Quit:
                    QuitState();
                    break;
            }
        }

        public void ChangeState(States states) => _currentState = states;
        private void WinState()
        {
            WinMinigame?.Invoke();
            crazyMoveController.ChangeMovePreset();
        }


        private void InitializeState()
        {
            _currentLevel = Instantiate(levels[Random.Range(0, levels.Count)],game.transform);
            _player.transform.position = _player.startingPos;
            _currentState = States.Game;
        }

        private void GameState()
        {
            
        }

        private void PauseState()
        {
            _player.Pause();
        }

        private void ResetState()
        {
            Destroy(_currentLevel);
            _currentLevel = Instantiate(levels[Random.Range(0, levels.Count)],game.transform);
            _player.transform.position=_player.startingPos;
            _currentState = States.Game;
        }

        private void QuitState()
        {
            Destroy(_currentLevel);
        }
    }
}

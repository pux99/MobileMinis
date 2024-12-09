using System;
using System.Collections;
using System.Collections.Generic;
using Minigames.GeneralUse;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Minigames.SimonSays
{
    public class SimonButtonManager : MonoBehaviour
    {
        [SerializeField] private ColorSequenceManager colorSequenceManager;
        [SerializeField] private List<SimonButton> buttons;
        [SerializeField] private float delay;
        [SerializeField] private SimonSaysMinigameController minigameController;
        private int _index;
        private List<int> _sequence;
        public Action WinMinigame;
        public Action LostMinigame;


        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            play(colorSequenceManager.GenerateNextInSequence());
        }

        public void play(List<int> Order)
        {
            _index = 0;
            _sequence = Order;
            StartCoroutine(PlaySequence());
        }

        IEnumerator PlaySequence()
        {
            foreach (var button in buttons)
            {
                button.button.interactable = false;
            }

            foreach (var t in _sequence)
            {
                yield return new WaitForSeconds(delay/2);
                buttons[t].Highlight();
                yield return new WaitForSeconds(delay);
                buttons[t].BackToNormal();
            }
            foreach (var button in buttons)
            {
                button.button.interactable = true;
            }
        }

        public void ButtonPress(int buttonID)
        {
            if (buttonID == _sequence[_index])
            {
                if (_index == _sequence.Count - 1)
                {
                    play(colorSequenceManager.GenerateNextInSequence());
                    _index = 0;
                    WinMinigame?.Invoke();
                    Debug.Log("next level");
                }
                else
                    _index++;
            }
            else
            {
                LostMinigame?.Invoke();
                colorSequenceManager.ResetSequence();
                play(colorSequenceManager.GenerateNextInSequence());
            }
        }
    }
}

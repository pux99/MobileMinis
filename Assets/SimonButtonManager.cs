using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SimonButtonManager : MonoBehaviour
{

    [SerializeField] private List<Button> Buttons;
    [SerializeField] private float delay;
    private int _index;
    private List<int> _sequence;

    public void play(List<int> Order)
    {
        _index = 0;
        StartCoroutine(PlaySequence());
        _sequence = Order;
    }

    IEnumerator PlaySequence()
    {
        foreach (var button in Buttons)
        {
            button.interactable = false;
        }

        for (int i = 0; i < _sequence.Count; i++)
        {
            //Buttons[_sequence[i]].enabled;
            yield return new WaitForSeconds(delay);
            //Buttons[_sequence[i]].disable;
        }
    }

    public void ButtonPress(int ButtonID)
    {
        if (ButtonID == _sequence[_index])
        {
            if (_index == _sequence.Count - 1)
                Debug.Log("win");
                //nextTurn
            else
                _index++;
        }
        else
        {
            //clear sequence , nextTurn
        }
    }
}

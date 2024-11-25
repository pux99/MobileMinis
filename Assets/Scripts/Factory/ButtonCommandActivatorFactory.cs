using System;
using Command;
using TMPro;
using UnityEngine;

namespace Factory
{
    public class ButtonCommandActivatorFactory : MonoBehaviour, IButtonFactory
    {
        [SerializeField] private GameObject buttonPrefab;

        public GameObject CreateButton<T, T1>(string buttonText, Vector2 position, T Tcommand,
            T1 Targument)
        {
            Type command = typeof(T);
            Type argument = typeof(T1);
            if (command == typeof(SoCommand) && argument == typeof(string))
            {
                GameObject newButton = Instantiate(buttonPrefab);
                
                TextMeshProUGUI buttonTextComponent = newButton.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonTextComponent != null)
                {
                    buttonTextComponent.text = buttonText;
                }

                var commandActivator = newButton.GetComponent<CommandActivator>();
                commandActivator.Command = Tcommand as SoCommand;
                commandActivator.Arguments = Targument as string;
                
                return newButton;
            }
            Debug.LogWarning(this.ToString() + " Only accept calls with two arguments: T (an ICommand) and T1 (a string), You made at least one incorrect call; please revise it.");
            return new GameObject();
        }

        public GameObject CreateButton<T>(string buttonText, Vector2 position, T scriptsAttach)
        {
            Debug.LogWarning(this.ToString() + " Only accept calls with two arguments: T (an ICommand) and T1 (a string)");
            return new GameObject();
        }
        
        public GameObject CreateButton<T, T1, T2>(string buttonText, Vector2 position, T scriptsAttach,
            T1 scriptsAttach1, T2 scriptsAttach2)
        {
            Debug.LogWarning(this.ToString() + " Only accept calls with two arguments: T (an ICommand) and T1 (a string)");
            return new GameObject();
        }

        public GameObject CreateButton<T, T1, T2, T3>(string buttonText, Vector2 position, T scriptsAttach,
            T1 scriptsAttach1, T2 scriptsAttach2, T3 scriptsAttach3)
        {
            Debug.LogWarning(this.ToString() + " Only accept calls with two arguments: T (an ICommand) and T1 (a string)");
            return new GameObject();
        }
    }
}

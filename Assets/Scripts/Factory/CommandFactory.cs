using System;
using Command;
using TMPro;
using UnityEngine;

namespace Factory
{
    public class CommandFactory : MonoBehaviour, IButtonFactory
    {
        [SerializeField] private CommandActivator prefab;

        public GameObject CreateButton<TPrefab, T, T1>(TPrefab prefab, string buttonText, Vector2 position, T Tcommand,
            T1 Targument) where TPrefab : Component
        {
            Type command = typeof(T);
            Type argument = typeof(T1);
            if (Tcommand is SoCommand && argument == typeof(string))
            {
                TPrefab result = Instantiate(prefab);
                
                TextMeshProUGUI text = result.GetComponentInChildren<TextMeshProUGUI>();
                if (text != null)
                {
                    text.text = buttonText;
                }
                var commandComponent = result.GetComponent<CommandActivator>();
                commandComponent.Command = Tcommand as SoCommand;
                commandComponent.Arguments = Targument as string;
                
                return result.gameObject;
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

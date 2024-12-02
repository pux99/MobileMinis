using System;
using System.Collections.Generic;
using Command;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Factory
{
    public class CommandActivatorButtonMaker : MonoBehaviour
    {
        [SerializeField] private CommandFactory buttonFactory;
        [SerializeField] private Transform parent;
        [SerializeField] private List<Command> commands;
        [SerializeField] private CommandActivator buttonPrefab;

        public void CreateCommands()
        {
            parent.DetachChildren();
            foreach (var command in commands)
            {
                //GameObject button= buttonFactory.CreateButton<SoCommand, string>(command.buttonName, Vector2.zero, 
                //    command.soCommand, command.argument);
                GameObject button= buttonFactory.CreateButton(new List<Object>(){buttonPrefab,command.soCommand,
                    new CommandButtonFactoryVariables(command.buttonName,command.argument)});
                button.transform.SetParent(parent);
                button.transform.localScale=Vector3.one;
            }
        }
        
    }
    [Serializable]public class Command 
    {
        public string buttonName;
        public SoCommand soCommand;
        public string argument;
    }

    
}
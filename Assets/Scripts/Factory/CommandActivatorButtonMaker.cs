using System;
using System.Collections.Generic;
using Command;
using UnityEngine;

namespace Factory
{
    public class CommandActivatorButtonMaker : MonoBehaviour
    {
        [SerializeField] private ButtonCommandActivatorFactory buttonFactory;
        [SerializeField] private Transform parent;
        [SerializeField] private List<Command> commands;
        
        [Serializable]private class Command
        {
            public string buttonName;
            public SoCommand soCommand;
            public string argument;
        }

        public void CreateCommands()
        {
            parent.DetachChildren();
            foreach (var command in commands)
            {
                GameObject button= buttonFactory.CreateButton<SoCommand, string>(command.buttonName, Vector2.zero, 
                    command.soCommand, command.argument);
                button.transform.SetParent(parent);
                button.transform.localScale=Vector3.one;
            }
        }
    }

    
}
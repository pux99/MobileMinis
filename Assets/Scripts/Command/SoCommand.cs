using UnityEngine;

namespace Command
{
    public abstract class SoCommand : ScriptableObject ,ICommand
    {
        public string Name => name;

        public abstract void Execute();

        public abstract void Execute(string[] args);
    }
}

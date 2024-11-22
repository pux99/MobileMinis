namespace Command
{
    public interface ICommand
    {
        public string Name { get; }
        public void Execute();
        public void Execute(string[] args);
    }
}
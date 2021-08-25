namespace Managers.CommandManagers
{
    public delegate void Command();
    public struct CommandInfo
    {
        public CommandInfo(string name, Command command)
        {
            this.Name = name;
            this.Command = command;
        }

        public Command Command { get; set; }
        public string Name { get; set; }
    }
}
namespace Brace.Commands.Factory
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(string command, string argument, string[] parameters);
    }
}
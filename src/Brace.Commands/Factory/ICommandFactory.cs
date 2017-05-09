using Brace.DomainModel.Command;

namespace Brace.Commands.Factory
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(CommandInfo commandInfo);
    }
}
using Brace.DomainService.Command;

namespace Brace.Commands.Factory
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(CommandInfo commandInfo);
    }
}
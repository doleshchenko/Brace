using Brace.DomainService;

namespace Brace.Commands.Factory
{
    public class CommandFactory : ICommandFactory
    {
        private readonly ICommandLinker _commandLinker;
        private readonly ISingleInterfaceServiceProvider<ICommand> _commandProvider;

        public CommandFactory(ICommandLinker commandLinker, ISingleInterfaceServiceProvider<ICommand> commandProvider)
        {
            _commandLinker = commandLinker;
            _commandProvider = commandProvider;
        }

        public ICommand CreateCommand(string command, string argument, string[] parameters)
        {
            var commandType = _commandLinker.GetCommandType(command);
            var commandObject = _commandProvider.Resolve(commandType);
            commandObject.SetParameters(argument, parameters);
            return commandObject;
        }
    }
}
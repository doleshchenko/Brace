using System;
using Brace.DomainService;
using Brace.DomainService.TypeLinker;

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
            Type commandType;
            try
            {
                commandType = _commandLinker.GetCommandType(command);
            }
            catch (LinkerException e) when (e.Message.Contains("Invalid command identifier"))
            {
                commandType = _commandLinker.GetCommandType(CommandType.Unknown.ToString());
            }
            var commandObject = _commandProvider.Resolve(commandType);
            commandObject.SetParameters(command, argument, parameters);
            return commandObject;
        }
    }
}
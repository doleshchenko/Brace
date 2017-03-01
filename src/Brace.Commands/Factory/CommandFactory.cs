using System;

namespace Brace.Commands.Factory
{
    public class CommandFactory
    {
        private readonly CommandLinker _commandLinker;
        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(CommandLinker commandLinker, IServiceProvider serviceProvider)
        {
            _commandLinker = commandLinker;
            _serviceProvider = serviceProvider;
        }

        public ICommand CreateCommand(string command, string argument, string[] parameters)
        {
            var commandType = _commandLinker.GetCommandType(command);
            var commandObject = (ICommand) _serviceProvider.GetService(commandType);
            commandObject.SetParameters(argument, parameters);
            return commandObject;
        }
    }
}
using System;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands.Factory
{
    public class CommandFactory
    {
        private readonly CommandLinker _commandLinker;
        private readonly IDocumentProcessor _documentProcessor;

        public CommandFactory(CommandLinker commandLinker, IDocumentProcessor documentProcessor)
        {
            _commandLinker = commandLinker;
            _documentProcessor = documentProcessor;
        }

        public ICommand CreateCommand(string command, string argument, string[] parameters)
        {
            var commandType = _commandLinker.GetCommandType(command);
            return (ICommand)Activator.CreateInstance(commandType, _documentProcessor, argument, parameters);
        }
    }
}
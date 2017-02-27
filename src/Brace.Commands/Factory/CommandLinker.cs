using System;
using Brace.DomainService.TypeLinker;

namespace Brace.Commands.Factory
{
    public class CommandLinker : TypeLinker<CommandAttribute, ICommand, CommandType>
    {
        public Type GetCommandType(string command)
        {
            CommandType commandType;
            if (Enum.TryParse(command, out commandType))
            {
                throw new LinkerException($"Invalid command identifier - {command}. Cannot translate into CommandType.");
            }
            var type = _links[commandType];
            return type;
        }
    }
}
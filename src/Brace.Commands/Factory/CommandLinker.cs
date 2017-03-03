using System;
using System.Reflection;
using Brace.DomainService.TypeLinker;

namespace Brace.Commands.Factory
{
    public class CommandLinker : TypeLinker<CommandAttribute, ICommand, CommandType>
    {
        public CommandLinker(Assembly assembly)
        {
            Init(assembly);
        }

        public Type GetCommandType(string command)
        {
            CommandType commandType;
            if (!Enum.TryParse(command, true, out commandType))
            {
                throw new LinkerException($"Invalid command identifier - {command}. Cannot translate into CommandType.");
            }
            var type = _links[commandType];
            return type;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Brace.Commands.Factory
{
    public class CommandLinker
    {
        private readonly Dictionary<CommandType, Type> _commandTypes;

        public CommandLinker()
        {
            _commandTypes = new Dictionary<CommandType, Type>();
            var types = typeof(ICommand).GetTypeInfo().Assembly.GetTypes().Where(t => typeof(ICommand).IsAssignableFrom(t)).ToArray();
            foreach (var type in types)
            {
                var commandAttribute = type.GetTypeInfo().GetCustomAttributes<CommandAttribute>().FirstOrDefault();
                if (commandAttribute != null)
                {
                    if (_commandTypes.ContainsKey(commandAttribute.Type))
                    {
                        throw new CommandLinkerException($"Several commands are mapped to the same CommandType: {commandAttribute.Type}");
                    }
                    _commandTypes[commandAttribute.Type] = type;
                }
            }
        }

        public Type GetCommandType(string command)
        {
            CommandType commandType;
            if (Enum.TryParse(command, out commandType))
            {
                throw new CommandLinkerException($"Invalid command identifier - {command}. Cannot translate into CommandType.");
            }
            var type = _commandTypes[commandType];
            return type;
        }
    }
}
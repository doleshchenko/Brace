using System;
using Brace.DomainModel;

namespace Brace.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandAttribute : Attribute, ITypeLink<CommandType>
    {
        public CommandAttribute(CommandType type)
        {
            LinkKey = type;
        }

        public CommandType LinkKey { get; set; }
    }
}
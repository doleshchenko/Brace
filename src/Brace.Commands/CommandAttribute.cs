using System;

namespace Brace.Commands
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(CommandType type)
        {
            Type = type;
        }

        public CommandType Type { get; set; }
    }
}
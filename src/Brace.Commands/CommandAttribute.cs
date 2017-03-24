using System;
using Brace.DomainModel;
using Brace.DomainModel.DocumentProcessing;

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
        public ArchivistType[] AssociatedArchivists { get; set; }
    }
}
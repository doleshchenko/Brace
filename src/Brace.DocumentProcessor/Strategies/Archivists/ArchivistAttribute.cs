using System;
using Brace.DomainModel;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    public class ArchivistAttribute : Attribute, ITypeLink<ArchivistType>
    {
        public ArchivistAttribute(ArchivistType linkKey)
        {
            LinkKey = linkKey;
        }

        public ArchivistType LinkKey { get; set; }
    }
}
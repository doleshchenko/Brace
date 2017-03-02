using System;

namespace Brace.DomainModel.DocumentProcessing.Attributes
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
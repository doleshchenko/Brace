using System;

namespace Brace.DomainModel.DocumentProcessing.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ArchivistTypeDescriptionAttribute: Attribute
    {
        public ArchivistTypeDescriptionAttribute(string archivistActionName)
        {
            ArchivistActionName = archivistActionName;
        }

        public string ArchivistActionName { get; set; }
    }
}
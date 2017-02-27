using System;
using Brace.DomainModel;

namespace Brace.DomainService.DocumentProcessor
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DocumentProcessingStrategyAttribute : Attribute, ITypeLink<ActionType>
    {
        public DocumentProcessingStrategyAttribute(ActionType linkKey)
        {
            LinkKey = linkKey;
        }

        public ActionType LinkKey { get; set; }
    }
}
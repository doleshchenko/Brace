using System;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DocumentProcessingStrategyAttribute : Attribute
    {
        public DocumentProcessingStrategyAttribute(ActionType associatedWith)
        {
            AssociatedWith = associatedWith;
        }

        public ActionType AssociatedWith { get; set; }
    }
}
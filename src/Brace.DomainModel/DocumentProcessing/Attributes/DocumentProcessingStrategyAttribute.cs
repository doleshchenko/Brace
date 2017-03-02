using System;

namespace Brace.DomainModel.DocumentProcessing.Attributes
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
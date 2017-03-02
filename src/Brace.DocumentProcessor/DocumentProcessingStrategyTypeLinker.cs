using System;
using System.Reflection;
using Brace.DomainModel;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainService.DocumentProcessor;
using Brace.DomainService.TypeLinker;

namespace Brace.DocumentProcessor
{
    public class DocumentProcessingStrategyTypeLinker : 
        TypeLinker<DocumentProcessingStrategyAttribute, IDocumentProcessingStrategy, ActionType>, 
        IDocumentProcessingStrategyTypeLinker
    {
        public DocumentProcessingStrategyTypeLinker(Assembly assembly)
        {
            Init(assembly);
        }

        public Type GetStrategyType(ActionType actionType)
        {
            return GetLinkedTypeByKey(actionType);
        }
    }
}
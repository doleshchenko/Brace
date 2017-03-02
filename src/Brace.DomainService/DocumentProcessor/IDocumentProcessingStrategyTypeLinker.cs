using System;
using Brace.DomainModel;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategyTypeLinker
    {
        Type GetStrategyType(ActionType actionType);
    }
}
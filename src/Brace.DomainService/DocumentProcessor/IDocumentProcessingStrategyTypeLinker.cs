using System;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategyTypeLinker
    {
        Type GetStrategyType(ActionType actionType);
    }
}
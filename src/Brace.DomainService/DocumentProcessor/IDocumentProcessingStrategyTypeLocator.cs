using System;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategyTypeLocator
    {
        Type GetStrategyType(ActionType actionType);
    }
}
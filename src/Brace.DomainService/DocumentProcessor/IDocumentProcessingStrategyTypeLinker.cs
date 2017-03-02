using System;
using Brace.DomainModel;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategyTypeLinker
    {
        Type GetStrategyType(ActionType actionType);
    }
}
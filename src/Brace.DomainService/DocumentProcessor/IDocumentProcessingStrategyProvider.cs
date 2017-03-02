using System;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategyProvider
    {
        IDocumentProcessingStrategy GetStrategy(Type strategyType);
    }
}
using System;
using System.Reflection;
using Brace.DocumentProcessor.Exceptions;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor
{
    public class DocumentProcessingStrategyProvider : IDocumentProcessingStrategyProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public DocumentProcessingStrategyProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDocumentProcessingStrategy GetStrategy(Type strategyType)
        {
            if (!typeof(IDocumentProcessingStrategy).IsAssignableFrom(strategyType))
            {
                throw new DocumentProcessorException($"Invalid strategy type {strategyType}. Cannot be cast to IDocumentProcessingStrategy.");
            }
            return (IDocumentProcessingStrategy)_serviceProvider.GetService(strategyType);
        }
    }
}
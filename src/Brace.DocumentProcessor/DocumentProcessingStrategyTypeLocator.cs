using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Brace.DocumentProcessor.Exceptions;
using Brace.DocumentProcessor.Strategies;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor
{
    public class DocumentProcessingStrategyTypeLocator : IDocumentProcessingStrategyTypeLocator
    {
        private readonly Dictionary<ActionType, Type> _processingStrategies;
        public DocumentProcessingStrategyTypeLocator(Assembly assembly)
        {
             _processingStrategies = new Dictionary<ActionType, Type>();

            var strategyTypes = assembly.GetTypes().Where(t => typeof(IDocumentProcessingStrategy).IsAssignableFrom(t)).ToArray();
            
            foreach (var strategyType in strategyTypes)
            {
                var classAttributes = strategyType.GetTypeInfo().GetCustomAttributes<DocumentProcessingStrategyAttribute>().ToArray();
                if (classAttributes != null && classAttributes.Any())
                {
                    var processingStrategyMeta = classAttributes.First();
                    var actionType = processingStrategyMeta.AssociatedWith;
                    if (_processingStrategies.ContainsKey(actionType))
                    {
                        throw new DocumentProcessorException($"Document processing strategies configured incorrectly. Several strategies associated with the same Action Type - {actionType}. Please verify configuration.");
                    }
                    _processingStrategies.Add(actionType, strategyType);
                }
            }

            var allPossibleActions = Enum.GetValues(typeof(ActionType)).Cast<ActionType>().ToArray();
            if (_processingStrategies.Count != allPossibleActions.Length)
            {
                throw new DocumentProcessorException("Document processing strategies configured incorrectly. Strategies not defined for all action types.");
            }
        }

        public Type GetStrategyType(ActionType actionType)
        {
            if (!_processingStrategies.ContainsKey(actionType))
            {
                throw new DocumentProcessorException($"Document processing strategy is not defined for Action Type - {actionType}");
            }
            var strategyType = _processingStrategies[actionType];
            return strategyType;
        }
    }
}
using System;
using System.Threading.Tasks;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor
{
    public class DocumentProcessor : IDocumentProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDocumentProcessingStrategyTypeLinker _processingStrategyTypeLinker;

        public DocumentProcessor(IDocumentProcessingStrategyTypeLinker processingStrategyTypeLinker, IServiceProvider serviceProvider)
        {
            _processingStrategyTypeLinker = processingStrategyTypeLinker;
            _serviceProvider = serviceProvider;
        }

        public async Task<DocumentView> ProcessAsync(string documentName, ActionType action, string[] actionParameters)
        {
            var strategyType = _processingStrategyTypeLinker.GetStrategyType(action);
            var strategy = (IDocumentProcessingStrategy)_serviceProvider.GetService(strategyType);
            return await strategy.ProcessAsync(documentName, actionParameters);
        }
    }
}

using System;
using System.Threading.Tasks;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;
using Brace.Repository.Interface;

namespace Brace.DocumentProcessor
{
    public class DocumentProcessor : IDocumentProcessor
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentProcessingStrategyTypeLocator _processingStrategyTypeLocator;

        public DocumentProcessor(IDocumentRepository documentRepository, IDocumentProcessingStrategyTypeLocator processingStrategyTypeLocator)
        {
            _documentRepository = documentRepository;
            _processingStrategyTypeLocator = processingStrategyTypeLocator;
        }

        public async Task<Document> ProcessAsync(string documentName, ActionType action, string[] actionParameters)
        {
            var strategyType = _processingStrategyTypeLocator.GetStrategyType(action);
            var strategy = (IDocumentProcessingStrategy)Activator.CreateInstance(strategyType, _documentRepository);
            return await strategy.ProcessAsync(documentName, actionParameters);
        }
    }
}

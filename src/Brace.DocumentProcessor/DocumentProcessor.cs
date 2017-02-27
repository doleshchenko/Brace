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
        private readonly IDocumentProcessingStrategyTypeLinker _processingStrategyTypeLinker;

        public DocumentProcessor(IDocumentRepository documentRepository, IDocumentProcessingStrategyTypeLinker processingStrategyTypeLinker)
        {
            _documentRepository = documentRepository;
            _processingStrategyTypeLinker = processingStrategyTypeLinker;
        }

        public async Task<Document> ProcessAsync(string documentName, ActionType action, string[] actionParameters)
        {
            var strategyType = _processingStrategyTypeLinker.GetStrategyType(action);
            var strategy = (IDocumentProcessingStrategy)Activator.CreateInstance(strategyType, _documentRepository);
            return await strategy.ProcessAsync(documentName, actionParameters);
        }
    }
}

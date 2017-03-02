using System.Threading.Tasks;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor
{
    public class DocumentProcessor : IDocumentProcessor
    {
        private readonly IDocumentProcessingStrategyProvider _documentProcessingStrategyProvider;
        private readonly IDocumentProcessingStrategyTypeLinker _processingStrategyTypeLinker;

        public DocumentProcessor(
            IDocumentProcessingStrategyTypeLinker processingStrategyTypeLinker, 
            IDocumentProcessingStrategyProvider documentProcessingStrategyProvider)
        {
            _processingStrategyTypeLinker = processingStrategyTypeLinker;
            _documentProcessingStrategyProvider = documentProcessingStrategyProvider;
        }

        public async Task<DocumentView> ProcessAsync(string documentName, ActionType action, string[] actionParameters)
        {
            var strategyType = _processingStrategyTypeLinker.GetStrategyType(action);
            var strategy = _documentProcessingStrategyProvider.GetStrategy(strategyType);
            return await strategy.ProcessAsync(documentName, actionParameters);
        }
    }
}

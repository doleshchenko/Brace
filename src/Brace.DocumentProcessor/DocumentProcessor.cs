using System.Threading.Tasks;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor
{
    public class DocumentProcessor : IDocumentProcessor
    {
        private readonly ISingleInterfaceServiceProvider _singleInterfaceServiceProvider;
        private readonly IDocumentProcessingStrategyTypeLinker _processingStrategyTypeLinker;

        public DocumentProcessor(
            IDocumentProcessingStrategyTypeLinker processingStrategyTypeLinker, 
            ISingleInterfaceServiceProvider singleInterfaceServiceProvider)
        {
            _processingStrategyTypeLinker = processingStrategyTypeLinker;
            _singleInterfaceServiceProvider = singleInterfaceServiceProvider;
        }

        public async Task<DocumentView> ProcessAsync(string documentName, ActionType action, string[] actionParameters)
        {
            var strategyType = _processingStrategyTypeLinker.GetStrategyType(action);
            var strategy = _singleInterfaceServiceProvider.GetStrategy(strategyType);
            return await strategy.ProcessAsync(documentName, actionParameters);
        }
    }
}

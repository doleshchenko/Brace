using System.Linq;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainService;
using Brace.DomainService.DocumentProcessor;
using System.Threading.Tasks;

namespace Brace.DocumentProcessor
{
    public class DocumentProcessor : IDocumentProcessor
    {
        private readonly ISingleInterfaceServiceProvider<IDocumentProcessingStrategy> _strategyProvider;
        private readonly IDocumentProcessingStrategyTypeLinker _processingStrategyTypeLinker;

        public DocumentProcessor(
            IDocumentProcessingStrategyTypeLinker processingStrategyTypeLinker, 
            ISingleInterfaceServiceProvider<IDocumentProcessingStrategy> strategyProvider)
        {
            _processingStrategyTypeLinker = processingStrategyTypeLinker;
            _strategyProvider = strategyProvider;
        }

        public async Task<DocumentView> ProcessAsync(string subject, ActionType action, ActionParameter[] actionParameters)
        {
            var strategyType = _processingStrategyTypeLinker.GetStrategyType(action);
            var strategy = _strategyProvider.Resolve(strategyType);
            return await strategy.ProcessAsync(subject, actionParameters?
                .Select(it => new DocumentProcessingAction
                {
                    ActionName = it.Name,
                    RequiredData = it.Data
                }).ToArray());
        }
    }
}

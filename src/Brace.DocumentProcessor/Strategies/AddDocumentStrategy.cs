using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies
{
    [DocumentProcessingStrategy(ActionType.AddDocument)]
    public class AddDocumentStrategy : IDocumentProcessingStrategy
    {
        public async Task<DocumentView> ProcessAsync(string documentName, DocumentProcessingAction[] documentProcessingActions)
        {
            throw new System.NotImplementedException();
        }
    }
}
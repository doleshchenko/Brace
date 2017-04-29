using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategy
    {
        Task<DocumentView> ProcessAsync(string documentName, DocumentProcessingAction[] documentProcessingActions);
    }
}
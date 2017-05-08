using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Subjects;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategy
    {
        Task<DocumentView> ProcessAsync(Subject subject, DocumentProcessingAction[] documentProcessingActions);
    }
}
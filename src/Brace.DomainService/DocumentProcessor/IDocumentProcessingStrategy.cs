using System.Threading.Tasks;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategy
    {
        Task<DocumentView> ProcessAsync(Subject subject, DocumentProcessingAction[] documentProcessingActions);
    }
}
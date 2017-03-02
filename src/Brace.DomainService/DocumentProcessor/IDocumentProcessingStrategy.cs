using System.Threading.Tasks;
using Brace.DomainModel;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategy
    {
        Task<DocumentView> ProcessAsync(string documentName, string[] actions);
    }
}
using System.Threading.Tasks;
using Brace.DomainModel;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategy
    {
        Task<DocumentView> ProcessAsync(string documentName, string[] actions);
    }
}
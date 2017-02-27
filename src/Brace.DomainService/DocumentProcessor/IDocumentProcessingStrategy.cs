using System.Threading.Tasks;
using Brace.DomainModel;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessingStrategy
    {
        Task<Document> ProcessAsync(string documentName, string[] actionParameters);
    }
}
using System.Threading.Tasks;
using Brace.DomainModel;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessor
    {
        Task<Document> ProcessAsync(string documentName, ActionType action, string[] actionParameters);
    }
}
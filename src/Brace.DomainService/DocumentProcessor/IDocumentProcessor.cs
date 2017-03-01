using System.Threading.Tasks;
using Brace.DomainModel;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessor
    {
        Task<DocumentView> ProcessAsync(string documentName, ActionType action, string[] actionParameters);
    }
}
using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IDocumentProcessor
    {
        Task<DocumentView> ProcessAsync(string documentName, ActionType action, ActionParameter[] actionParameters);
    }
}
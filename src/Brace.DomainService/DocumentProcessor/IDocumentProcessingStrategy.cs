using System.Threading.Tasks;
using Brace.DomainModel;

namespace Brace.DocumentProcessor.Strategies
{
    public interface IDocumentProcessingStrategy
    {
        Task<Document> ProcessAsync(string documentName, string[] actionParameters);
    }
}
using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Stub.ValidProcessingStrategies
{
    [DocumentProcessingStrategy(ActionType.Print)]
    public class PrintProcessingStrategyStub : IDocumentProcessingStrategy
    {
        public Task<Document> ProcessAsync(string documentName, string[] actionParameters)
        {
            return null;
        }
    }
}
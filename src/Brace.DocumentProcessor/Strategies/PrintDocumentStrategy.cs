using System.Threading.Tasks;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;
using Brace.Repository.Interface;

namespace Brace.DocumentProcessor.Strategies
{
    [DocumentProcessingStrategy(ActionType.Print)]
    public class PrintDocumentStrategy : IDocumentProcessingStrategy
    {
        private readonly IDocumentRepository _documentRepository;

        public PrintDocumentStrategy(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<DocumentView> ProcessAsync(string documentName, string[] actions)
        {
            var document = await _documentRepository.FindDocumentAsync(documentName);

            return new DocumentView {Content = document.Content};
        }
    }
}
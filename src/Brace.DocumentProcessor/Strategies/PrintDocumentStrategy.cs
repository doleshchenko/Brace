using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainService.DocumentProcessor;
using Brace.Repository.Interface;

namespace Brace.DocumentProcessor.Strategies
{
    [DocumentProcessingStrategy(ActionType.Print)]
    public class PrintDocumentStrategy : IDocumentProcessingStrategy
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IArchivistFactory _archivistFactory;

        public PrintDocumentStrategy(IDocumentRepository documentRepository, IArchivistFactory archivistFactory)
        {
            _documentRepository = documentRepository;
            _archivistFactory = archivistFactory;
        }

        public async Task<DocumentView> ProcessAsync(string documentName, string[] actions)
        {
            var document = await _documentRepository.FindDocumentAsync(documentName);
            if (document == null)
            {
                return null;
            }
            var archivist = _archivistFactory.CreateArchivistChain(actions);
            var rethinkedDocument = archivist.Rethink(document);
            return new DocumentView {Content = rethinkedDocument.Content};
        }
    }
}
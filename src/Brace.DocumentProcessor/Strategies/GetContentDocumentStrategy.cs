using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Brace.DomainService.DocumentProcessor;
using Brace.Repository.Interface;

namespace Brace.DocumentProcessor.Strategies
{
    [DocumentProcessingStrategy(ActionType.GetContent)]
    public class GetContentDocumentStrategy : IDocumentProcessingStrategy
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IArchivistFactory _archivistFactory;

        public GetContentDocumentStrategy(IDocumentRepository documentRepository, IArchivistFactory archivistFactory)
        {
            _documentRepository = documentRepository;
            _archivistFactory = archivistFactory;
        }

        public async Task<DocumentView> ProcessAsync(string documentName, string[] actions)
        {
            var document = await _documentRepository.FindDocumentAsync(documentName);
            if (document == null)
            {
                return new DocumentView<DocumentPlainContent>
                {
                    Content = new DocumentPlainContent {PlainText = $"document '{documentName}' not found"},
                    Type = DocumentViewType.Warning
                };
            }
            var archivist = _archivistFactory.CreateArchivistChain(actions);
            var rethinkedDocument = archivist.Rethink(document);
            return new DocumentView<DocumentPlainContent>
            {
                Content = new DocumentPlainContent {PlainText = rethinkedDocument.Content},
                Type = DocumentViewType.Ok
            };
        }
    }
}
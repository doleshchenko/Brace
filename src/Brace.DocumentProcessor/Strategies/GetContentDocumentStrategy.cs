using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Brace.DomainModel.DocumentProcessing.Subjects;
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

        public async Task<DocumentView> ProcessAsync(Subject subject, DocumentProcessingAction[] documentProcessingActions)
        {
            var document = await _documentRepository.FindDocumentAsync(subject.Id);
            if (document == null)
            {
                return new DocumentView<DocumentPlainContent>
                {
                    Content = new DocumentPlainContent {PlainText = $"document '{subject.Id}' not found"},
                    Type = DocumentViewType.Warning
                };
            }
            var archivist = _archivistFactory.CreateArchivistChain(documentProcessingActions);
            var rethinkedDocument = archivist.Rethink(document);
            return new DocumentView<DocumentPlainContent>
            {
                Content = new DocumentPlainContent {PlainText = rethinkedDocument.Content},
                Type = DocumentViewType.Ok
            };
        }
    }
}
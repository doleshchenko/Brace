using System.Linq;
using System.Threading.Tasks;
using Brace.DocumentProcessor.Strategies.Archivists.Factory;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Brace.DomainService.DocumentProcessor;
using Brace.Repository.Interface;

namespace Brace.DocumentProcessor.Strategies
{
    [DocumentProcessingStrategy(ActionType.Enumerate)]
    public class EnumerateDocumentStrategy : IDocumentProcessingStrategy
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IArchivistFactory _archivistFactory;

        public EnumerateDocumentStrategy(IDocumentRepository documentRepository, IArchivistFactory archivistFactory)
        {
            _documentRepository = documentRepository;
            _archivistFactory = archivistFactory;
        }

        public async Task<DocumentView> ProcessAsync(Subject subject, DocumentProcessingAction[] documentProcessingActions)
        {
            var allDocumnents = await _documentRepository.GetDocumentsListAsync();
            var archivist = _archivistFactory.CreateArchivistChain(documentProcessingActions);
            var resultedDocuments = allDocumnents.Select(it =>
                            new Document
                            {
                                Id = it.Id,
                                Name = it.Name,
                                IsProtected = it.IsProtected,
                                IsVisible = it.IsVisible
                            })
                    .Select(document => archivist.Rethink(document))
                    .Where(result => result != null);
            var content = new DocumentListContent<DocumentDescriptionContent>();
            content.AddRange(resultedDocuments.Select(it =>
                            new DocumentDescriptionContent
                            {
                                DocumentName = it.Name,
                                IsDocumentProtected = it.IsProtected,
                                IsDocumentVisible = it.IsVisible
                            }));
            var documentView = new DocumentView<DocumentListContent<DocumentDescriptionContent>>
            {
                Content = content,
                Type = DocumentViewType.Ok
            };
            return documentView;
        }
    }
}
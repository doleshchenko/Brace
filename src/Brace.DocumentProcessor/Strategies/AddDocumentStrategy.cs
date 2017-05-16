using System.Threading.Tasks;
using Brace.DocumentProcessor.Exceptions;
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
    [DocumentProcessingStrategy(ActionType.AddDocument)]
    public class AddDocumentStrategy : IDocumentProcessingStrategy
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IArchivistFactory _archivistFactory;

        public AddDocumentStrategy(IDocumentRepository documentRepository, IArchivistFactory archivistFactory)
        {
            _documentRepository = documentRepository;
            _archivistFactory = archivistFactory;
        }

        public async Task<DocumentView> ProcessAsync(Subject subject, DocumentProcessingAction[] documentProcessingActions)
        {
            if (subject is null)
            {
                return new DocumentView<DocumentProcessingResultContent>
                {
                    Type = DocumentViewType.Warning,
                    Content = new DocumentProcessingResultContent
                    {
                        ProcessingResultType = DocumentProcessingResultType.AddFailed,
                        Description = "The new document is empty. Cannot create an empty document."
                    }
                };
            }
            if (subject is NewDocument newDocument)
            {
                var document = new Document
                {
                    Id = newDocument.Id,
                    Content = newDocument.Content
                };
                await _documentRepository.AddAsync(document);
                var archivist = _archivistFactory.CreateArchivistChain(documentProcessingActions);
                var result = archivist.Rethink(document);
                await _documentRepository.UpdateAsync(result);
                return new DocumentView<DocumentProcessingResultContent>
                {
                    Type = DocumentViewType.Ok,
                    Content = new DocumentProcessingResultContent {ProcessingResultType = DocumentProcessingResultType.Added}
                };
            }
            throw new DocumentProcessingStrategyException($"Invalid subject type - {subject.GetType()}. It should be {typeof(NewDocument)}.");
        }
    }
}
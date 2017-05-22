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
    [DocumentProcessingStrategy(ActionType.UpdateDocument)]
    public class UpdateDocumentStrategy : IDocumentProcessingStrategy
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IArchivistFactory _archivistFactory;

        public UpdateDocumentStrategy(IDocumentRepository documentRepository, IArchivistFactory archivistFactory)
        {
            _documentRepository = documentRepository;
            _archivistFactory = archivistFactory;
        }

        public async Task<DocumentView> ProcessAsync(Subject subject, DocumentProcessingAction[] documentProcessingActions)
        {
            if (subject is UpdateDocumentSubject updateDocumentSubject)
            {
                var document = await _documentRepository.FindDocumentAsync(updateDocumentSubject.Id);
                if (document == null)
                {
                    return new DocumentView<DocumentProcessingResultContent>
                    {
                        Content = new DocumentProcessingResultContent { ProcessingResultType = DocumentProcessingResultType.UpdateFailed, Description = $"document '{subject.Id}' not found" },
                        Type = DocumentViewType.Warning
                    };
                }

                var archivist = _archivistFactory.CreateArchivistChain(documentProcessingActions);
                if (updateDocumentSubject.ContentUpdateRequired)
                {
                    document.Content = updateDocumentSubject.Content;
                }
                var result = archivist.Rethink(document);
                await _documentRepository.UpdateAsync(result);
                return new DocumentView<DocumentProcessingResultContent>
                {
                    Type = DocumentViewType.Ok,
                    Content = new DocumentProcessingResultContent { ProcessingResultType = DocumentProcessingResultType.Added }
                };
            }
            throw new DocumentProcessingStrategyException($"Invalid subject type - {subject.GetType()}. It should be {typeof(UpdateDocumentSubject)}.");
        }
    }
}
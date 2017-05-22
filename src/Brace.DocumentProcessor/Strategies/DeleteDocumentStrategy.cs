using System.Threading.Tasks;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Brace.DomainService.DocumentProcessor;
using Brace.Repository.Interface;

namespace Brace.DocumentProcessor.Strategies
{
    [DocumentProcessingStrategy(ActionType.DeleteDocument)]
    public class DeleteDocumentStrategy : IDocumentProcessingStrategy
    {
        private readonly IDocumentRepository _documentRepository;

        public DeleteDocumentStrategy(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<DocumentView> ProcessAsync(Subject subject, DocumentProcessingAction[] documentProcessingActions)
        {
            await _documentRepository.DeleteAsync(subject.Id);
            return new DocumentView<DocumentProcessingResultContent>
            {
                Type = DocumentViewType.Ok,
                Content = new DocumentProcessingResultContent { ProcessingResultType = DocumentProcessingResultType.Deleted }
            };
        }
    }
}
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists.Factory
{
    public interface IArchivistFactory
    {
        IArchivist CreateArchivistChain(DocumentProcessingAction[] actionsToPerform);
    }
}
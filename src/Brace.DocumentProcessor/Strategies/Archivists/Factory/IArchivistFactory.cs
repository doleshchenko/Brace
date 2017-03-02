using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists.Factory
{
    public interface IArchivistFactory
    {
        IArchivist CreateArchivistChain(string[] actionsToPerform);
    }
}
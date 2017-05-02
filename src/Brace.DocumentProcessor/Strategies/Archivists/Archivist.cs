using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    public abstract class Archivist : IArchivist
    {
        protected IArchivist _successor;
        
        
        public IArchivist Successor
        {
            get => _successor;
            set => _successor = value;
        }

        public abstract Document Rethink(Document document);
    }
}
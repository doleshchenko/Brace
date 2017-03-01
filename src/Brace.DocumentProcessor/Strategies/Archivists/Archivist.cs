using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    public abstract class Archivist : IArchivist
    {
        protected IArchivist _successor;

        protected Archivist()
        {
        }

        protected Archivist(IArchivist successor)
        {
            _successor = successor;
        }

        public IArchivist Successor
        {
            get { return _successor; }
            set { _successor = value; }
        }

        public abstract Document Rethink(Document document);
    }
}
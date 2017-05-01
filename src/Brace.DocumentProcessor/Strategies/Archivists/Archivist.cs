using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    public abstract class Archivist : IArchivist
    {
        protected IArchivist _successor;
        protected string _configuration;
        
        public IArchivist Successor
        {
            get => _successor;
            set => _successor = value;
        }

        public virtual void Configure(string configuration)
        {
            _configuration = configuration;
        }

        public abstract Document Rethink(Document document);
    }
}
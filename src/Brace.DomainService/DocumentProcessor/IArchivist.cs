using Brace.DomainModel.DocumentProcessing;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IArchivist
    {
        void Configure(string configuration);
        Document Rethink(Document document);
        IArchivist Successor { get; set; }
    }
}
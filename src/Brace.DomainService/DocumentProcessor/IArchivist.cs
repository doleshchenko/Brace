using Brace.DomainModel;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IArchivist
    {
        Document Rethink(Document document);
    }
}
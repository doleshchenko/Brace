using Brace.DomainModel;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IArchivist
    {
        Document Rethink(Document document);
    }
}
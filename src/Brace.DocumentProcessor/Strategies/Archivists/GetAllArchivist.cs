using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.GetAll)]
    public class GetAllArchivist : Archivist
    {
        public GetAllArchivist(IArchivist successor) : base(successor)
        {
        }

        public override Document Rethink(Document document)
        {
            if (_successor != null)
            {
                return _successor.Rethink(document);
            }
            return document;
        }
    }
}
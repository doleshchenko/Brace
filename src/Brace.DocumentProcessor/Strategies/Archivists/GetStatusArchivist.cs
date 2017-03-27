using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.GetStatus)]
    public class GetStatusArchivist : Archivist
    {
        public GetStatusArchivist(IArchivist successor) : base(successor)
        {
        }

        public override Document Rethink(Document document)
        {
            throw new System.NotImplementedException();
        }
    }
}
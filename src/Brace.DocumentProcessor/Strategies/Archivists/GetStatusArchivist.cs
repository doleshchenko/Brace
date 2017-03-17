using Brace.DomainModel.DocumentProcessing;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.GetStatus)]
    public class GetStatusArchivist : Archivist
    {
        public override Document Rethink(Document document)
        {
            throw new System.NotImplementedException();
        }
    }
}
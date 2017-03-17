using Brace.DomainModel.DocumentProcessing;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.Encrypt)]
    public class EncryptArchivist: Archivist
    {
        public override Document Rethink(Document document)
        {
            throw new System.NotImplementedException();
        }
    }
}
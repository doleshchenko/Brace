using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.Encrypt)]
    public class EncryptArchivist: Archivist
    {
        public EncryptArchivist(IArchivist successor) : base(successor)
        {
        }

        public override Document Rethink(Document document)
        {
            throw new System.NotImplementedException();
        }
    }
}
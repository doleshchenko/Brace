using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.Decrypt)]
    public class DecryptArchivist : Archivist
    {
        public DecryptArchivist(IArchivist successor) : base(successor)
        {
        }

        public override Document Rethink(Document document)
        {
            throw new System.NotImplementedException();
        }
    }
}
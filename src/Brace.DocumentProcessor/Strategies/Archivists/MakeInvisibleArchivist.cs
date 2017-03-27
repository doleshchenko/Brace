using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.MakeInvisible)]
    public class MakeInvisibleArchivist : Archivist
    {
        public MakeInvisibleArchivist(IArchivist successor) : base(successor)
        {
        }

        public override Document Rethink(Document document)
        {
            throw new System.NotImplementedException();
        }
    }
}
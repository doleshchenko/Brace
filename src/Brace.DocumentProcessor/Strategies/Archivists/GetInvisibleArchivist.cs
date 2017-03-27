using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.GetInvisible)]
    public class GetInvisibleArchivist : Archivist
    {
        public GetInvisibleArchivist(IArchivist successor) : base(successor)
        {
        }

        public override Document Rethink(Document document)
        {
            if (document.IsVisible)
            {
                return null;
            }
            if (_successor != null)
            {
                return _successor.Rethink(document);
            }
            return document;
        }
    }
}
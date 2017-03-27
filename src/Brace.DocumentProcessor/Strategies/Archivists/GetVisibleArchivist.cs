using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.GetVisible)]
    public class GetVisibleArchivist : Archivist
    {
        public GetVisibleArchivist(IArchivist successor) : base(successor)
        {
        }

        public override Document Rethink(Document document)
        {
            if (document == null || !document.IsVisible)
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
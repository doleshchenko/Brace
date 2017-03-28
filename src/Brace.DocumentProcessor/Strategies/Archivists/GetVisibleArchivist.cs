using Brace.DomainModel.DocumentProcessing;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.GetVisible)]
    public class GetVisibleArchivist : Archivist
    {
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
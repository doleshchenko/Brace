using Brace.DomainModel.DocumentProcessing;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.MakeVisible)]
    public class MakeVisibleArchivist : Archivist
    {
        public override Document Rethink(Document document)
        {
            if (document != null)
            {
                document.IsVisible = true;
            }
            if (_successor != null)
            {
                return _successor.Rethink(document);
            }
            return document;
        }
    }
}
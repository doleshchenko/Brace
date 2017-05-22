using Brace.DomainModel.DocumentProcessing;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.MakeInvisible)]
    public class MakeInvisibleArchivist : Archivist
    {
        public override Document Rethink(Document document)
        {
            if (document != null)
            {
                document.IsVisible = false;
            }
            if (_successor != null)
            {
                return _successor.Rethink(document);
            }
            return document;
        }
    }
}
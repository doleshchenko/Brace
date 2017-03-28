using Brace.DomainModel.DocumentProcessing;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [DomainModel.DocumentProcessing.Attributes.Archivist(ArchivistType.DoNothing)]
    public class DoNothingArhivist : Archivist
    {
        
        public override Document Rethink(Document document)
        {
            if (_successor != null)
            {
                return _successor.Rethink(document);
            }
            return document;
        }
    }
}
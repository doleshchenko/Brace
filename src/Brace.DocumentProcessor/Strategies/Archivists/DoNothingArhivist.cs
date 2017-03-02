using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists
{
    [Archivist(ArchivistType.DoNothing)]
    public class DoNothingArhivist : Archivist
    {
        public DoNothingArhivist()
        {
        }

        public DoNothingArhivist(IArchivist successor) : base(successor)
        {
        }

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
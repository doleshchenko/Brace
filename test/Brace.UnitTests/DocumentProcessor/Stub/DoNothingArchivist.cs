using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.UnitTests.DocumentProcessor.Stub
{
    public class DoNothingArchivist: Archivist
    {
        public override Document Rethink(Document document)
        {
            return document;
        }
    }
}
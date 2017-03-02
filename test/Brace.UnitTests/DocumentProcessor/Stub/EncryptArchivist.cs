using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.UnitTests.DocumentProcessor.Stub
{
    public class EncryptArchivist : Archivist
    {
        public override Document Rethink(Document document)
        {
            return document;
        }
    }
}
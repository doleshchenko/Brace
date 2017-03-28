using System;
using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.UnitTests.DocumentProcessor.Mock
{
    public class DecryptArchivistMock : Archivist
    {
        public override Document Rethink(Document document)
        {
            throw new NotImplementedException();
        }

        public IArchivist GetSuccessor()
        {
            return Successor;
        }
    }
}
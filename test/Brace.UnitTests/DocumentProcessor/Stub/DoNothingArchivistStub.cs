﻿using Brace.DocumentProcessor.Strategies.Archivists;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.UnitTests.DocumentProcessor.Stub
{
    public class DoNothingArchivistStub: Archivist
    {
        public DoNothingArchivistStub(IArchivist successor) : base(successor)
        {
        }

        public override Document Rethink(Document document)
        {
            return document;
        }
    }
}
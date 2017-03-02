using System;
using Brace.DomainModel;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IArchivistLinker
    {
        Type GetArchivistType(ArchivistType key);
    }
}
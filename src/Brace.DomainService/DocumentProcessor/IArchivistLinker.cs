using System;
using Brace.DomainModel;

namespace Brace.DomainService.DocumentProcessor
{
    public interface IArchivistLinker
    {
        Type GetArchivistType(ArchivistType key);
    }
}
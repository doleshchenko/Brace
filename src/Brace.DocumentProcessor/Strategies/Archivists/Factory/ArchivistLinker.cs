using System;
using System.Reflection;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;
using Brace.DomainService.TypeLinker;

namespace Brace.DocumentProcessor.Strategies.Archivists.Factory
{
    public class ArchivistLinker : TypeLinker<ArchivistAttribute, IArchivist, ArchivistType>, IArchivistLinker
    {
        public ArchivistLinker()
        {
            Init(typeof(Archivist).GetTypeInfo().Assembly);
        }

        public Type GetArchivistType(ArchivistType key)
        {
            return GetLinkedTypeByKey(key);
        }
    }
}
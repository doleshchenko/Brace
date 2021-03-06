﻿using System;
using System.Reflection;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainService.DocumentProcessor;
using Brace.DomainService.TypeLinker;

namespace Brace.DocumentProcessor.Strategies.Archivists.Factory
{
    public class ArchivistLinker : TypeLinker<ArchivistAttribute, IArchivist, ArchivistType>, IArchivistLinker
    {
        public ArchivistLinker(Assembly assembly)
        {
            Init(assembly);
        }

        public Type GetArchivistType(ArchivistType key)
        {
            return GetLinkedTypeByKey(key);
        }
    }
}
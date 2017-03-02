using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Brace.DocumentProcessor.Exceptions;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists.Factory
{
    public class ArchivistFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IArchivistLinker _archivistLinker;

        public ArchivistFactory(IServiceProvider serviceProvider, IArchivistLinker archivistLinker)
        {
            _serviceProvider = serviceProvider;
            _archivistLinker = archivistLinker;
        }

        public IArchivist CreateArchivistChain(string[] actionsToPerform)
        {
            var archivistTypes = GetArchivistTypesByActions(actionsToPerform);
            var archivistObjectTypes = archivistTypes.Select(archivistType => _archivistLinker.GetArchivistType(archivistType)).ToArray();
            foreach (var archivistObjectType in archivistObjectTypes)
            {
                //current
                //previous
            }
            return null;
        }

        private IEnumerable<ArchivistType> GetArchivistTypesByActions(string[] actions)
        {
            if (actions == null || !actions.Any())
            {
                yield return ArchivistType.DoNothing;
            }
            var fields = typeof(ArchivistType).GetTypeInfo().GetFields().ToArray();
            foreach (var fieldInfo in fields)
            {
                var descriptionAttribute = fieldInfo.GetCustomAttribute<ArchivistTypeDescriptionAttribute>();
                if (descriptionAttribute != null)
                {
                    var relatedAction = descriptionAttribute.ArchivistActionName;
                    if (actions.Any(it => it == relatedAction))
                    {
                        yield return (ArchivistType) fieldInfo.GetValue(null);
                    }
                    else
                    {
                        throw new DocumentProcessorException($"Invalid action (command parameter) {relatedAction}. Cannot find corresponding Archivist type.");
                    }
                }
            }
        }

    }
}
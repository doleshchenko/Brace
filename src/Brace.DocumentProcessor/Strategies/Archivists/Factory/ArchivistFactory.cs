using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Brace.DocumentProcessor.Exceptions;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Attributes;
using Brace.DomainService;
using Brace.DomainService.DocumentProcessor;

namespace Brace.DocumentProcessor.Strategies.Archivists.Factory
{
    public class ArchivistFactory : IArchivistFactory
    {
        private readonly ISingleInterfaceServiceProvider<IArchivist> _achivistProvider;
        private readonly IArchivistLinker _archivistLinker;

        public ArchivistFactory(ISingleInterfaceServiceProvider<IArchivist> achivistProvider, IArchivistLinker archivistLinker)
        {
            _achivistProvider = achivistProvider;
            _archivistLinker = archivistLinker;
        }

        public IArchivist CreateArchivistChain(DocumentProcessingAction[] actionsToPerform)
        {
            var archivistTypes = GetArchivistTypesByActions(actionsToPerform?.Select(it => it.ActionName).ToArray());
            var archivistObjectTypes = archivistTypes
                .Select(archivistType => (ArchivistType: _archivistLinker.GetArchivistType(archivistType.ActionType), ActionName: archivistType.ActionName))
                .ToArray();
            IArchivist previous = null;
            IArchivist root = null;
            foreach (var archivistObjectType in archivistObjectTypes)
            {
                var current = _achivistProvider.Resolve(archivistObjectType.ArchivistType);
                if (current is IConfigurableArchivist)
                {
                    ((IConfigurableArchivist)current).Configure(actionsToPerform.Single(it => it.ActionName == archivistObjectType.ActionName).RequiredData);
                }
                if (root == null)
                {
                    root = current;
                }
                if (previous != null)
                {
                    ((Archivist)previous).Successor = current;
                }
                previous = current;
            }
            return root;
        }

        private IEnumerable<(ArchivistType ActionType, string ActionName)> GetArchivistTypesByActions(string[] actions)
        {
            if (actions == null || !actions.Any())
            {
                return new[]{ (ActionType: ArchivistType.DoNothing, ActionName: string.Empty)};
            }
            var fields = typeof(ArchivistType).GetTypeInfo().GetFields().ToArray();
            var fieldsWithAttributes = fields.Select(it => new { Attribure = it.GetCustomAttribute<ArchivistTypeDescriptionAttribute>(), Field = it })
                    .Where(it => it.Attribure != null)
                    .ToArray();

            var wrongItems = actions.Except(fieldsWithAttributes.Select(it => it.Attribure.ArchivistActionName)).ToArray();
            if (wrongItems.Any())
            {
                throw new DocumentProcessorException($"Invalid action (command parameter) \"{string.Join(",", wrongItems)}\". Cannot find corresponding Archivist type.");
            }
            var result = new List<(ArchivistType ActionType, string ActionName)>();
            foreach (var fieldAndAttribute in fieldsWithAttributes)
            {
                if (actions.Any(it => it == fieldAndAttribute.Attribure.ArchivistActionName))
                {
                    result.Add( (ActionType: (ArchivistType)fieldAndAttribute.Field.GetValue(null),
                        ActionName: fieldAndAttribute.Attribure.ArchivistActionName));
                }
            }
            return result;
        }
    }
}
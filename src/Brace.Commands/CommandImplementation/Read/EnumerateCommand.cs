using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands.CommandImplementation.Read
{
    [Command(CommandType.Enumerate, AssociatedArchivists = new[] {ArchivistType.GetInvisible, ArchivistType.GetVisible})]
    public class EnumerateCommand : CommandBase
    {
        public EnumerateCommand(IDocumentProcessor documentProcessor) : base(documentProcessor)
        {
        }

        protected override ActionType GetActionType()
        {
            return ActionType.Enumerate;
        }
    }
}
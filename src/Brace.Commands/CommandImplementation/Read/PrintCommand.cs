using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands.CommandImplementation.Read
{
    [Command(CommandType.Print)]
    public class PrintCommand : CommandBase
    {
        public PrintCommand(IDocumentProcessor documentProcessor) :base(documentProcessor)
        {
        }

        protected override ActionType GetActionType()
        {
            return ActionType.Print;
        }
    }
}

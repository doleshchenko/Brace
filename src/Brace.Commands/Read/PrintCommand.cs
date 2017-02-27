using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands.Read
{
    [Command(CommandType.Print)]
    public class PrintCommand : CommandBase
    {
        public PrintCommand(IDocumentProcessor documentProcessor, string argument, string[] parameters) :base(documentProcessor, argument, parameters)
        {
        }

        protected override ActionType GetActionType()
        {
            return ActionType.Print;
        }
    }
}

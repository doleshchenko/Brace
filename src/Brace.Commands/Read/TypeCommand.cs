using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands.Read
{
    public class TypeCommand : BraceCommand
    {
        public TypeCommand(IDocumentProcessor documentProcessor, string argument, string[] parameters) :base(documentProcessor, argument, parameters)
        {
        }

        public override CommandType Type => CommandType.Type;
        protected override ActionType GetActionType()
        {
            return ActionType.Print;
        }
    }
}

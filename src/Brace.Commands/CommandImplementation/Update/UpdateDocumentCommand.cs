using Brace.Commands.Validation;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands.CommandImplementation.Update
{
    [Command(CommandType.UpdateDocument, AssociatedArchivists = new[] { ArchivistType.Encrypt, ArchivistType.Decrypt, ArchivistType.MakeInvisible, ArchivistType.MakeVisible })]
    public class UpdateDocumentCommand : CommandBase
    {
        public UpdateDocumentCommand(IDocumentProcessor documentProcessor) : base(documentProcessor)
        {
        }

        public override CommandValidationResult Validate()
        {
            var validationResult = base.Validate();
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (Subject.IsNullOrNothing(Subject))
            {
                validationResult.IsValid = false;
                validationResult.ValidationMessage = "Invalid command subject: it's null or empty.";
            }
            return validationResult;
        }

        protected override ActionType GetActionType()
        {
            return ActionType.UpdateDocument;
        }
    }
}
﻿using Brace.Commands.Validation;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands.CommandImplementation.Add
{
    [Command(CommandType.AddDocument, AssociatedArchivists = new[] {ArchivistType.Encrypt, ArchivistType.MakeInvisible})]
    public class AddDocumentCommand : CommandBase
    {
        public AddDocumentCommand(IDocumentProcessor documentProcessor) : base(documentProcessor)
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
            return ActionType.AddDocument;
        }
    }
}
﻿using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands.CommandImplementation.Read
{
    [Command(CommandType.GetContent, AssociatedArchivists = new[]{ArchivistType.Decrypt})]
    public class GetContentCommand : CommandBase
    {
        public GetContentCommand(IDocumentProcessor documentProcessor) :base(documentProcessor)
        {
        }

        public override CommandValidationResult Validate()
        {
            var validationResult = base.Validate();
            if (!validationResult.IsValid)
            {
                return validationResult;
            }

            if (string.IsNullOrWhiteSpace(Argument))
            {
                validationResult.IsValid = false;
                validationResult.ValidationMessage = "Invalid command argument: document name can't be empty";
            }
            return validationResult;
        }

        protected override ActionType GetActionType()
        {
            return ActionType.GetContent;
        }
    }
}
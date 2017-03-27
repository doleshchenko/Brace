﻿using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands.CommandImplementation.Read
{
    [Command(CommandType.Emumerate, AssociatedArchivists = new[]
    {
        ArchivistType.GetAll, ArchivistType.GetInvisible, ArchivistType.GetVisible, ArchivistType.GetStatus
    })]
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
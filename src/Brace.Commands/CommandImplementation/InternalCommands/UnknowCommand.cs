using System;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;

namespace Brace.Commands.CommandImplementation.InternalCommands
{
    [Command(CommandType.Unknown)]
    public class UnknowCommand : ICommand
    {
        public UnknowCommand()
        {
            CreationDate = DateTime.Now;
        }

        public async Task<DocumentView> ExecuteAsync()
        {
            return await Task.FromResult(new DocumentView<DocumentPlainContent>
                {
                    Content = new DocumentPlainContent {PlainText = $"unknown command [{CommandText}]"},
                    Type = DocumentViewType.Warning
                });
        }

        public void SetParameters(string commandId, Subject subject, Modifier[] modifiers)
        {
            Subject = subject;
            Modifiers = modifiers;
            CommandText = commandId;
        }

        public DateTime CreationDate { get; }
        public Subject Subject { get; private set; }
        public Modifier[] Modifiers { get; private set; }
        public string CommandText { get; private set; }
        public CommandValidationResult Validate()
        {
            //Unknown command is always valid
            return CommandValidationResult.Valid;
        }
    }
}
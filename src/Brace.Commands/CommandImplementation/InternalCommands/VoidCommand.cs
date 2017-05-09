using System;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;

namespace Brace.Commands.CommandImplementation.InternalCommands
{
    [Command(CommandType.Void)]
    public class VoidCommand : ICommand
    {
        public VoidCommand()
        {
            CreationDate = DateTime.Now;
        }

        public async Task<DocumentView> ExecuteAsync()
        {
            return await Task.FromResult(new DocumentView<DocumentPlainContent>
                {
                    Content = new DocumentPlainContent {PlainText = "void command"},
                    Type = DocumentViewType.Warning
                });
        }

        public void SetParameters(string commandText, Subject subject, Predicate[] predicates)
        {
        }

        public DateTime CreationDate { get; }
        public Subject Subject => null;
        public Predicate[] Predicates => null;
        public string CommandText => null;
        public CommandValidationResult Validate()
        {
            //Void command is always valid
            return CommandValidationResult.Valid;
        }
    }
}
using System;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Brace.DomainService.Command;

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

        public void SetParameters(string commandText, string subject, CommandParameter[] parameters)
        {
        }

        public DateTime CreationDate { get; }
        public string Subject => null;
        public CommandParameter[] Parameters => null;
        public string CommandText => null;
        public CommandValidationResult Validate()
        {
            //Void command is always valid
            return CommandValidationResult.Valid;
        }
    }
}
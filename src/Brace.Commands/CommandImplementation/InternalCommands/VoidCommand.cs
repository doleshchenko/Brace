using System;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing;

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
            return await Task.FromResult(new DocumentView<string> {Content = "void command", Type = DocumentViewType.Warning});
        }

        public void SetParameters(string commandText, string argument, string[] parameters)
        {
        }

        public DateTime CreationDate { get; }
        public string Argument => null;
        public string[] Parameters => null;
        public string CommandText => null;
        public CommandValidationResult Validate()
        {
            //Void command is always valid
            return CommandValidationResult.Valid;
        }
    }
}
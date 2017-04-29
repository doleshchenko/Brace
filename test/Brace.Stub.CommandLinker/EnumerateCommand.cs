using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainService.Command;

namespace Brace.Stub.CommandLinker
{
    [Command(CommandType.Enumerate)]
    public class EnumerateCommand : ICommand
    {
        public string Subject { get; }
        public string CommandText { get; }
        public CommandParameter[] Parameters { get; }
        public DateTime CreationDate { get; }
        public Task<DocumentView> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public CommandValidationResult Validate()
        {
            throw new NotImplementedException();
        }

        public void SetParameters(string commandText, string subject, CommandParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
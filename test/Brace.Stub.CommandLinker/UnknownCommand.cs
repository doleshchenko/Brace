using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainService.Command;

namespace Brace.Stub.CommandLinker
{
    [Command(CommandType.Unknown)]
    public class UnknownCommand : ICommand
    {
        public Task<DocumentView> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
        
        public DateTime CreationDate { get; }
        public string Subject { get; }
        public CommandParameter[] Parameters { get; }
        public string CommandText { get; }
        public void SetParameters(string commandText, string subject, CommandParameter[] parameters)
        {
            throw new NotImplementedException();
        }
        public CommandValidationResult Validate()
        {
            throw new NotImplementedException();
        }
    }
}
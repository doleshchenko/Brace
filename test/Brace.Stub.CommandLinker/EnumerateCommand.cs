using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;

namespace Brace.Stub.CommandLinker
{
    [Command(CommandType.Enumerate)]
    public class EnumerateCommand : ICommand
    {
        public string Argument { get; }
        public string CommandText { get; }
        public string[] Parameters { get; }
        public DateTime CreationDate { get; }
        public Task<DocumentView> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public CommandValidationResult Validate()
        {
            throw new NotImplementedException();
        }

        public void SetParameters(string commandText, string argument, string[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
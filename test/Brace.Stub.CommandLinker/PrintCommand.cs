using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.Stub.CommandLinker
{
    [Command(CommandType.Print)]
    public class PrintCommand : ICommand
    {
        public Task<DocumentView> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
        public DateTime CreationDate { get; }
        public string Argument { get; }
        public string[] Parameters { get; }
        public string CommandText { get; }
        public void SetParameters(string commandText, string argument, string[] parameters)
        {
            throw new NotImplementedException();
        }
        public CommandValidationResult Validate()
        {
            throw new NotImplementedException();
        }
    }
}

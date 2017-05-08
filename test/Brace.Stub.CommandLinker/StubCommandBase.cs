using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Subjects;
using Brace.DomainService.Command;

namespace Brace.Stub.CommandLinker
{
    public abstract class StubCommandBase : ICommand
    {
        public Subject Subject => throw new NotImplementedException();
        public string CommandText => throw new NotImplementedException();
        public CommandParameter[] Parameters => throw new NotImplementedException();
        public DateTime CreationDate => throw new NotImplementedException();
        public Task<DocumentView> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public CommandValidationResult Validate()
        {
            throw new NotImplementedException();
        }

        public void SetParameters(string commandText, Subject subject, CommandParameter[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
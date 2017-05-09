using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.Validation;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing.Decorator;

namespace Brace.Stub.CommandLinker
{
    public abstract class StubCommandBase : ICommand
    {
        public Subject Subject => throw new NotImplementedException();
        public string CommandText => throw new NotImplementedException();
        public Predicate[] Predicates => throw new NotImplementedException();
        public DateTime CreationDate => throw new NotImplementedException();
        public Task<DocumentView> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public CommandValidationResult Validate()
        {
            throw new NotImplementedException();
        }

        public void SetParameters(string commandText, Subject subject, Predicate[] predicates)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Subjects;
using Brace.DomainService.Command;

namespace Brace.Commands
{
    public interface ICommand
    {
        Subject Subject { get; }
        string CommandText { get; }
        CommandParameter[] Parameters { get; }
        DateTime CreationDate { get; }
        Task<DocumentView> ExecuteAsync();
        CommandValidationResult Validate();
        void SetParameters(string commandText, Subject subject, CommandParameter[] parameters);
    }
}

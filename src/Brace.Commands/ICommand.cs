using System;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainService.Command;

namespace Brace.Commands
{
    public interface ICommand
    {
        string Subject { get; }
        string CommandText { get; }
        CommandParameter[] Parameters { get; }
        DateTime CreationDate { get; }
        Task<DocumentView> ExecuteAsync();
        CommandValidationResult Validate();
        void SetParameters(string commandText, string subject, CommandParameter[] parameters);
    }
}

using System;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;

namespace Brace.Commands
{
    public interface ICommand
    {
        string Argument { get; }
        string CommandText { get; }
        string[] Parameters { get; }
        DateTime CreationDate { get; }
        Task<DocumentView> ExecuteAsync();
        CommandValidationResult Validate();
        void SetParameters(string commandText, string argument, string[] parameters);
    }
}

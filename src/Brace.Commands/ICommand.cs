using System;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.Command;
using Brace.DomainModel.Command.Subjects;
using Brace.DomainModel.DocumentProcessing.Decorator;

namespace Brace.Commands
{
    public interface ICommand
    {
        string CommandText { get; }
        Subject Subject { get; }
        Modifier[] Modifiers { get; }
        DateTime CreationDate { get; }
        Task<DocumentView> ExecuteAsync();
        CommandValidationResult Validate();
        void SetParameters(string commandId, Subject subject, Modifier[] modifiers);
    }
}

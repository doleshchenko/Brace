using System;
using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.Commands
{
    public interface ICommand
    {
        string Argument { get; }
        string CommandText { get; }
        string[] Parameters { get; }
        DateTime CreationDate { get; }
        Task<DocumentView> ExecuteAsync();
        void SetParameters(string commandText, string argument, string[] parameters);
    }
}

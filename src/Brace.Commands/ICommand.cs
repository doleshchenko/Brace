using System;
using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.Commands
{
    public interface ICommand
    {
        Task<DocumentView> ExecuteAsync();
        void SetParameters(string argument, string[] parameters);
        DateTime CreationDate { get; }
        string Argument { get; }
        string[] Parameters { get; }
    }
}

using System;
using System.Threading.Tasks;
using Brace.DomainModel;

namespace Brace.Commands
{
    public interface ICommand
    {
        Task<Document> ExecuteAsync();
        DateTime CreationDate { get; }
        string Argument { get; }
        string[] Parameters { get; }
    }
}

using System;
using System.Threading.Tasks;
using Brace.DomainModel;

namespace Brace.Commands
{
    public interface IBraceCommand
    {
        Task<Document> ExecuteAsync();
        string Name { get; }
        CommandType Type { get; }
        DateTime CreationDate { get; }
        string Argument { get; }
        string[] Parameters { get; }
    }
}

using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.Stub.CommandLinker
{
    [Command(CommandType.Print)]
    public class PrintCommand : ICommand
    {
        public Task<DocumentView> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public void SetParameters(string argument, string[] parameters)
        {
            throw new NotImplementedException();
        }

        public DateTime CreationDate { get; }
        public string Argument { get; }
        public string[] Parameters { get; }
    }
}

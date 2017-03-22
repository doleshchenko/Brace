using System;
using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.Commands
{
    [Command(CommandType.Void)]
    public class VoidCommand: ICommand
    {
        public VoidCommand()
        {
            CreationDate = DateTime.Now;
        }

        public async Task<DocumentView> ExecuteAsync()
        {
            return await Task.FromResult(new DocumentView {Content = "void command", Type = DocumentViewType.Warning});
        }

        public void SetParameters(string argument, string[] parameters)
        {
        }

        public DateTime CreationDate { get; }
        public string Argument => null;
        public string[] Parameters => null;
    }
}
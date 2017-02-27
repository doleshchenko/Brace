using System;
using System.Threading.Tasks;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected readonly IDocumentProcessor _documentProcessor;
        private readonly string _argument;
        private readonly string[] _parameters;

        protected CommandBase(IDocumentProcessor documentProcessor, string argument, string[] parameters)
        {
            _documentProcessor = documentProcessor;
            _argument = argument;
            _parameters = parameters;
            CreationDate = DateTime.Now;
        }
        public DateTime CreationDate { get; }
        public string Argument => _argument;
        public string[] Parameters => _parameters;
       
        public virtual async Task<Document> ExecuteAsync()
        {
            return await _documentProcessor.ProcessAsync(_argument, GetActionType(), _parameters);
        }

        protected abstract ActionType GetActionType();
        
    }
}
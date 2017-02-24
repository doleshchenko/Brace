using System;
using System.Threading.Tasks;
using Brace.DomainModel;
using Brace.DomainService.DocumentProcessor;
using Brace.TypeExtension;

namespace Brace.Commands
{
    public abstract class BraceCommand : IBraceCommand
    {
        protected readonly IDocumentProcessor _documentProcessor;
        private readonly string _argument;
        private readonly string[] _parameters;

        protected BraceCommand(IDocumentProcessor documentProcessor, string argument, string[] parameters)
        {
            _documentProcessor = documentProcessor;
            _argument = argument;
            _parameters = parameters;
            CreationDate = DateTime.Now;
        }
        public DateTime CreationDate { get; }
        public virtual string Name => Type.ToStringUpper();
        public string Argument => _argument;
        public string[] Parameters => _parameters;
        public abstract CommandType Type { get; }

        public virtual async Task<Document> ExecuteAsync()
        {
            return await _documentProcessor.ProcessAsync(_argument, GetActionType(), _parameters);
        }

        protected abstract ActionType GetActionType();
        
    }
}
﻿using System;
using System.Threading.Tasks;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainService.DocumentProcessor;

namespace Brace.Commands
{
    public abstract class CommandBase : ICommand
    {
        protected readonly IDocumentProcessor _documentProcessor;
        private string _argument;
        private string[] _parameters;
        private string _commandText;

        protected CommandBase(IDocumentProcessor documentProcessor)
        {
            _documentProcessor = documentProcessor;
            CreationDate = DateTime.Now;
        }
        public DateTime CreationDate { get; }
        public string CommandText => _commandText;
        public string Argument => _argument;
        public string[] Parameters => _parameters;
       
        public virtual async Task<DocumentView> ExecuteAsync()
        {
            return await _documentProcessor.ProcessAsync(_argument, GetActionType(), _parameters);
        }

        protected abstract ActionType GetActionType();
        public void SetParameters(string commandText, string argument, string[] parameters)
        {
            _argument = argument;
            _parameters = parameters;
            _commandText = commandText;
        }
    }
}
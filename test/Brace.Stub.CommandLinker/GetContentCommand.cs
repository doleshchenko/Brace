﻿using System;
using System.Threading.Tasks;
using Brace.Commands;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing;
using Brace.DomainModel.DocumentProcessing.Decorator;

namespace Brace.Stub.CommandLinker
{
    [Command(CommandType.GetContent)]
    public class GetContentCommand : ICommand
    {
        public Task<DocumentView> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
        public DateTime CreationDate { get; }
        public string Argument { get; }
        public string[] Parameters { get; }
        public string CommandText { get; }
        public void SetParameters(string commandText, string argument, string[] parameters)
        {
            throw new NotImplementedException();
        }
        public CommandValidationResult Validate()
        {
            throw new NotImplementedException();
        }
    }
}

﻿using System;
using System.Threading.Tasks;
using Brace.Commands.Validation;
using Brace.DomainModel.DocumentProcessing;

namespace Brace.Commands.CommandImplementation.InternalCommands
{
    [Command(CommandType.Unknown)]
    public class UnknowCommand : ICommand
    {
        public UnknowCommand()
        {
            CreationDate = DateTime.Now;
        }

        public async Task<DocumentView> ExecuteAsync()
        {
            return await Task.FromResult(new DocumentView<string> { Content = $"unknown command [{CommandText}]", Type = DocumentViewType.Warning });
        }

        public void SetParameters(string commandText, string argument, string[] parameters)
        {
            Argument = argument;
            Parameters = parameters;
            CommandText = commandText;
        }

        public DateTime CreationDate { get; }
        public string Argument { get; private set; }
        public string[] Parameters { get; private set; }
        public string CommandText { get; private set; }
        public CommandValidationResult Validate()
        {
            //Unknown command is always valid
            return CommandValidationResult.Valid;
        }
    }
}
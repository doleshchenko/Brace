﻿using System;
using System.Threading.Tasks;
using Brace.Commands.Factory;
using Brace.DomainModel.Command;
using Brace.DomainModel.DocumentProcessing.Decorator;
using Brace.DomainModel.DocumentProcessing.Decorator.Content;
using Brace.Interpretation;
using Brace.Models;
using Microsoft.AspNetCore.Mvc;

namespace Brace.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private readonly ICommandFactory _commandFactory;
        private readonly ICommandInterpreter _commandInterpreter;

        public DocumentsController(ICommandFactory commandFactory, ICommandInterpreter commandInterpreter)
        {
            _commandFactory = commandFactory;
            _commandInterpreter = commandInterpreter;
        }

        /// <summary>
        /// Command examples:
        /// add {"name" : "some text for my document", "content" : "" } -encrypt[pass] -makeinvisible
        /// getcontent {doc name} -encrypt[pass]
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Command command)
        {
            var interpretation = _commandInterpreter.Interpret(command.CommandText);
            var commandInfo = new CommandInfo
            {
                Command = interpretation.Command,
                Subject = interpretation.Subject,
                Modifiers = interpretation.Modifiers
            };
            var concreteCommand = _commandFactory.CreateCommand(commandInfo);
            var  validationResult = concreteCommand.Validate();
            if (!validationResult.IsValid)
            {
                return Ok(new CommandExecutionResult
                {
                    Content = new DocumentPlainContent { PlainText = validationResult.ValidationMessage},
                    Type = CommandExecutionResultType.Warning
                });
            }
            var result = await concreteCommand.ExecuteAsync();
            return Ok(Convert(result));
        }

        private CommandExecutionResult Convert(DocumentView documentView)
        {
            var convertEnum = new Func<DocumentViewType, CommandExecutionResultType>(it =>
            {
                switch (it)
                {
                    case DocumentViewType.Ok:
                        return CommandExecutionResultType.Ok;
                    case DocumentViewType.Warning:
                        return CommandExecutionResultType.Warning;
                    case DocumentViewType.Error:
                        return CommandExecutionResultType.Error;
                }
                throw new ArgumentException($"Unknown DocumentViewType - {it}.");
            });
            var result = new CommandExecutionResult
            {
                Content = documentView.Content,
                Type = convertEnum(documentView.Type)
            };
            return result;
        }
    }
}
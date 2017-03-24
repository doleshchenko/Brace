using System;
using System.Threading.Tasks;
using Brace.Commands.Factory;
using Brace.DomainModel.DocumentProcessing;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Command command)
        {
            var interpretation = _commandInterpreter.Interpret(command.CommandText);
            var concreteCommand = _commandFactory.CreateCommand(interpretation.Command, interpretation.Argument, interpretation.Parameters);
            var  validationResult = concreteCommand.Validate();
            if (!validationResult.IsValid)
            {
                return Ok(new CommandExecutionResult
                {
                    Content = validationResult.ValidationMessage,
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
                        case DocumentViewType.Information:
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
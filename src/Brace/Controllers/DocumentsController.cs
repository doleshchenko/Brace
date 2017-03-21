using System;
using System.Threading.Tasks;
using Brace.Commands.Factory;
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
            var result = await concreteCommand.ExecuteAsync();
            return Ok(result);
        }
    }
}
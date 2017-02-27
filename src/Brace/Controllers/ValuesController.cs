using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Brace.Commands.Factory;
using Brace.Commands.Read;
using Brace.DomainService.DocumentProcessor;
using Brace.Interpretation;
using Microsoft.AspNetCore.Mvc;

namespace Brace.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ICommandInterpreter _commandInterpreter;
        private readonly CommandFactory _commandFactory;

        public ValuesController(ICommandInterpreter commandInterpreter, CommandFactory commandFactory)
        {
            _commandInterpreter = commandInterpreter;
            _commandFactory = commandFactory;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]string value)
        {
            var commandInterpretation = _commandInterpreter.Interpret(value);
            var command = _commandFactory.CreateCommand(commandInterpretation.Command, commandInterpretation.Argument, commandInterpretation.Parameters);
            var document = await command.ExecuteAsync();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

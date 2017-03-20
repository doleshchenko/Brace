using System.Threading.Tasks;
using Brace.Commands.Factory;
using Brace.DomainModel.DocumentProcessing;
using Brace.Models;
using Microsoft.AspNetCore.Mvc;

namespace Brace.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private readonly ICommandFactory _commandFactory;

        public DocumentsController(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        [HttpPost]
        public async Task<DocumentView> Post([FromBody]string command)
        {
            var c = _commandFactory.CreateCommand(command, null, null);
            var result = await c.ExecuteAsync();
            return result;
        }
    }
}
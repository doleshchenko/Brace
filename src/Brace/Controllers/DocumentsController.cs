using System.Threading.Tasks;
using Brace.Commands.Factory;
using Brace.DomainModel.DocumentProcessing;
using Microsoft.AspNetCore.Mvc;

namespace Brace.Controllers
{
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private readonly ICommandFactory _commandFactory;

        public DocumentsController(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        [HttpPost("{command}")]
        public async Task<DocumentView> Post(string command)
        {
            var c = _commandFactory.CreateCommand(command, null, null);
            var result = await c.ExecuteAsync();
            return result;
        }
    }
}
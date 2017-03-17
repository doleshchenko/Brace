using Microsoft.AspNetCore.Mvc;

namespace Brace.Controllers
{
    public class ConsoleController : Controller
    {
        [Route("console/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace Brace.Controllers
{
    public class ShellController : Controller
    {
        [Route("shell/index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
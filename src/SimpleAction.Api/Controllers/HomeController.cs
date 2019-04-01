using Microsoft.AspNetCore.Mvc;

namespace SimpleAction.Api.Controllers
{

    [Route("")]
    public class HomeController: Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from Simple Action API!"); 

    }
}
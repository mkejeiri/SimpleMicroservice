using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using SimpleAction.Common.Commands;

namespace SimpleAction.Api.Controllers {

    [Route ("[controller]")]
    public class UsersController : Controller {
        private readonly IBusClient _busClient;
        public UsersController (IBusClient busClient) {
            _busClient = busClient;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Post ([FromBody] CreateUser command) {
            System.Console.WriteLine ("Post method : creating user");
            await _busClient.PublishAsync (command);
            System.Console.WriteLine ("Post method : publishing user");
            return Accepted ();
        }

        [HttpGet ("")]
        public IActionResult Get () => Content ("Hello from UsersController API!");

    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using SimpleAction.Common.Commands;

namespace SimpleAction.Api.Controllers {

    [Route ("[controller]")]
    public class UserController : Controller {
        private readonly IBusClient _busClient;
        public UserController (IBusClient busClient) {
            _busClient = busClient;
        }

        [HttpPost ("register")]
        public async Task<IActionResult> Post ([FromBody] CreateUser command) {         
            await _busClient.PublishAsync (command);
            return Accepted();
        }

    }
}
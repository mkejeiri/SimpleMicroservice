using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleAction.Common.Commands;
using SimpleAction.Services.Identity.Services;

namespace SimpleAction.Services.Identity.Controllers {
    // [Route("api/[controller]")]
    [Route ("")]
    [ApiController]
    public class AccountController : Controller {
        private readonly IUserService _userService;

        public AccountController (IUserService userService) {
            _userService = userService;
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login ([FromBody] AuthenticateUser command) 
        => Json (await _userService.LoginAsync (command.Email, command.Password));
    }
}
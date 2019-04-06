using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using SimpleAction.Api.Services;
using SimpleAction.Common.Commands;

namespace SimpleAction.Api.Controllers {
    [Route ("[controller]")]
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //the whole controller is secured
    public class ActivitiesController : Controller {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;

        public ActivitiesController (IBusClient busClient, IActivityService activityService) {
            _busClient = busClient;
            _activityService = activityService;
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> Get (Guid Id) {
            var activityDto = await _activityService.GetActivityAsync (Id);
            if (activityDto == null)
            {
                return NotFound();
            }

            if (Guid.Parse(User.Identity.Name) != activityDto.UserId)
            {
                return Unauthorized();
            }
            
            return Json (await _activityService.GetActivityAsync (Id));
        }

        [HttpGet]
        // public IActionResult Get () => Content ("Secured");
        public async Task<IActionResult> Get () {
            return Json (await _activityService.GetActivitiesAsync (Guid.Parse (User.Identity.Name)));
        }

        [HttpPost ("")]
        public async Task<IActionResult> Post ([FromBody] CreateActivity command) {
            command.Id = Guid.NewGuid ();
            command.UserId = Guid.Parse (User.Identity.Name);
            command.CreatedAt = DateTime.UtcNow;
            await _busClient.PublishAsync (command);
            return Accepted ($"activities/{command.Id}");
        }

    }
}
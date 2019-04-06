using System.Threading.Tasks;
using SimpleAction.Api.Models;
using SimpleAction.Api.Repositories;
using SimpleAction.Common.Events;

namespace SimpleAction.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {           
            await _activityRepository.AddAsync(new Activity {
                Id=  @event.Id,
               UserId = @event.UserId,
               Name = @event.Name,
               Category = @event.Category,
               Description = @event.Description,
               CreatedAt = @event.CreatedAt
            });
          System.Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}
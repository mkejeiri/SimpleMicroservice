using System.Threading.Tasks;
using SimpleAction.Common.Events;

namespace SimpleAction.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        public async Task HandleAsync(ActivityCreated @event)
        {
            await Task.CompletedTask;
            System.Console.WriteLine($"Activity created: {@event.Name}");
        }
    }
}
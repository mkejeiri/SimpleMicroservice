using System.Threading.Tasks;
using SimpleAction.Common.Events;

namespace SimpleAction.Api.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        public Task HandleAsync(UserCreated @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
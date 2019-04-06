using System.Threading.Tasks;
using SimpleAction.Common.Events;

namespace SimpleAction.Api.Handlers {
    public class UserAuthenticatedHandler : IEventHandler<UserAuthenticated> {
        public async Task HandleAsync (UserAuthenticated @event) {
            await Task.CompletedTask;
            System.Console.WriteLine ($"Activity created: {@event.Email}");
        }
    }
}
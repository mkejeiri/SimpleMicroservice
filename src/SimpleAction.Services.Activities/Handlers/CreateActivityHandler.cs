using System.Threading.Tasks;
using RawRabbit;
using SimpleAction.Common.Commands;
using SimpleAction.Common.Events;

namespace SimpleAction.Services.Activities.Handlers {
    public class CreateActivityHandler : ICommandHandler<CreateActivity> {
        private readonly IBusClient _busClient;

        public CreateActivityHandler (IBusClient busClient) {
            _busClient = busClient;
        }

        //Here where contravariance comes to play (<in T> keyword)
        /*
        Assignment compatibility is reversed :   
            An object that is instantiated with a less derived type argument is assigned to an object instantiated with 
            a more derived type argument (use `in` keyword for generics).
        */
        public async Task HandleAsync (CreateActivity createActivity) {

            System.Console.WriteLine ($"creating activity :{createActivity.Name} ");
            await _busClient.PublishAsync (new ActivityCreated (createActivity.Id, createActivity.Category, createActivity.Name, createActivity.Description, createActivity.CreatedAt ));
        }
    }
}
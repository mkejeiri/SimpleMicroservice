using System;
using System.Threading.Tasks;
using RawRabbit;
using Microsoft.Extensions.Logging;
using SimpleAction.Common.Commands;
using SimpleAction.Common.Events;
using SimpleAction.Common.Exceptions;
using SimpleAction.Services.Activities.Services;

namespace SimpleAction.Services.Activities.Handlers {
    public class CreateActivityHandler : ICommandHandler<CreateActivity> {
        private readonly IBusClient _busClient;
        private readonly IActivityService _activityService;
        private readonly ILogger _logger;

        public CreateActivityHandler (IBusClient busClient, IActivityService activityService,
        ILogger<CreateActivity>  logger) {
            _busClient = busClient;
            _activityService = activityService;
            _logger = logger;
        }

        //Here where contravariance comes to play (<in T> keyword)
        /*
        Assignment compatibility is reversed :   
            An object that is instantiated with a less derived type argument is assigned to an object instantiated with 
            a more derived type argument (use `in` keyword for generics).
        */
        public async Task HandleAsync (CreateActivity createActivity) {

            _logger.LogInformation($"creating activity :{createActivity.Name} ");
            
            try {
                await _activityService.AddAsync (createActivity.Id, createActivity.UserId, createActivity.Category, createActivity.Name,
                    createActivity.Description, createActivity.CreatedAt);
                await _busClient.PublishAsync (new ActivityCreated (createActivity.Id,createActivity.UserId, createActivity.Category, createActivity.Name, createActivity.Description, createActivity.CreatedAt));
            } catch (ActionException ex) {
                 await _busClient.PublishAsync (new CreateActivityRejected(createActivity.Id,ex.Code,ex.Message ));
                 _logger.LogInformation(ex.Message);
            }
            catch (Exception ex) {
                 await _busClient.PublishAsync (new CreateActivityRejected(createActivity.Id,"Error",ex.Message ));
                 _logger.LogInformation(ex.Message);
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RawRabbit;
using SimpleAction.Common.Commands;
using SimpleAction.Common.Events;
using SimpleAction.Common.Exceptions;
using SimpleAction.Services.Identity.Services;

namespace SimpleAction.Services.Identity.Handlers {
    public class CreateUserHandler : ICommandHandler<CreateUser> {
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public CreateUserHandler (IBusClient busClient, IUserService userService,
         ILogger<CreateUserHandler> logger) {
            _busClient = busClient; 
            _userService = userService;
            _logger = logger;
        }
        //TODO: 
        public async Task HandleAsync (CreateUser command) {
            _logger.LogInformation ($"creating User :{command.Name} ");
            try {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);
               await _busClient.PublishAsync (new UserCreated (command.Email, command.Name));
            } catch (ActionException ex) {
                 await _busClient.PublishAsync (new CreateUserRejected(command.Email,ex.Code,ex.Message ));
                 _logger.LogInformation(ex.Message);
            }
            catch (Exception ex) {
                 await _busClient.PublishAsync (new CreateUserRejected(command.Email,"Error",ex.Message ));
                 _logger.LogInformation(ex.Message);
            }          
        }

    }
}
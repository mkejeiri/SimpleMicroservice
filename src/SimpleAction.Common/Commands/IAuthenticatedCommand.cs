using System;

namespace SimpleAction.Common.Commands {
    public interface IAuthenticatedCommand : ICommand {
        Guid UserId { get; set; }
    }
}
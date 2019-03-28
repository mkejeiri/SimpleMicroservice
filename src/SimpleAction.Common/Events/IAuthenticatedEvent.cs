using System;

namespace SimpleAction.Common.Events
{
    public interface IAuthenticatedEvent : IEvent
    {
         Guid UserId { get; set; } 
    }
}
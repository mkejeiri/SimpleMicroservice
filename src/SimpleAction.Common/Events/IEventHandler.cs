using System.Threading.Tasks;

namespace SimpleAction.Common.Events {
    public interface IEventHandler<in T> where T : IEvent {
        Task HandleAsync (T @event);
    }
}
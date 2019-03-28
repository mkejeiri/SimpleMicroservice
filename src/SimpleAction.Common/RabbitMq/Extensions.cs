using System.Reflection;
using System.Threading.Tasks;
using RawRabbit;
using RawRabbit.Pipe;
using SimpleAction.Common.Commands;
using SimpleAction.Common.Events;

namespace SimpleAction.Common.RabbitMq {
    public static class Extensions {
        public static Task WithCommandHandlerAsync<TCommand> (this IBusClient bus, ICommandHandler<TCommand> handler) where TCommand : ICommand 
        => bus.SubscribeAsync<TCommand> (
            msg => handler.HandleAsync (msg),
            ctx => ctx.UseSubscribeConfiguration (cfg => cfg.FromDeclaredQueue (queue => queue.WithName (GetQueueName<TCommand> ())))
        );


        public static Task WithEventHandlerAsync<TEvent> (this IBusClient bus, IEventHandler<TEvent> handler) where TEvent : IEvent 
        => bus.SubscribeAsync<TEvent> (
            msg => handler.HandleAsync (msg),
            ctx => ctx.UseSubscribeConfiguration (cfg => cfg.FromDeclaredQueue (queue => queue.WithName (GetQueueName<TEvent> ())))
        );

        private static string GetQueueName<T> () => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";
    }
}
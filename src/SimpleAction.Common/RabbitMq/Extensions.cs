using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;
using SimpleAction.Common.Commands;
using SimpleAction.Common.Events;

namespace SimpleAction.Common.RabbitMq {
    public static class Extensions {
        public static Task WithCommandHandlerAsync<TCommand> (this IBusClient bus, ICommandHandler<TCommand> handler) where TCommand : ICommand => bus.SubscribeAsync<TCommand> (
            msg => handler.HandleAsync (msg),
            ctx => ctx.UseSubscribeConfiguration (cfg => cfg.FromDeclaredQueue (queue => queue.WithName (GetQueueName<TCommand> ())))
        );

        public static Task WithEventHandlerAsync<TEvent> (this IBusClient bus, IEventHandler<TEvent> handler) where TEvent : IEvent => bus.SubscribeAsync<TEvent> (
            msg => handler.HandleAsync (msg),
            ctx => ctx.UseSubscribeConfiguration (cfg => cfg.FromDeclaredQueue (queue => queue.WithName (GetQueueName<TEvent> ())))
        );

        private static string GetQueueName<T> () => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        public static void AddRabbitMq (this IServiceCollection services, IConfiguration config) {
            var options = new RabbitMqOptions ();
            var section = config.GetSection ("rabbitmq");
            section.Bind (options);
            var client = RawRabbitFactory.CreateSingleton (new RawRabbitOptions {
                ClientConfiguration = options
            });
            services.AddSingleton<IBusClient> (_ => client);
        }
    }
}
using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using SimpleAction.Common.Commands;
using SimpleAction.Common.Events;
using SimpleAction.Common.RabbitMq;

namespace SimpleAction.Common.Services {
    public class ServiceHost : IServiceHost {
        private readonly IWebHost _webhost;

        public ServiceHost (IWebHost webhost) {
            _webhost = webhost;
        }

        public void Run () => _webhost.Run ();

        public static HostBuilder Create<TStartUp> (string[] args) where TStartUp : class {
            Console.Title = typeof (TStartUp).Namespace;
            var config = new ConfigurationBuilder ()
                .AddEnvironmentVariables ()
                .AddCommandLine (args)
                .Build ();

            var webHostBuilder = WebHost.CreateDefaultBuilder (args)
                .UseConfiguration (config)
                .UseStartup<TStartUp> ()
                .UseDefaultServiceProvider (options => options.ValidateScopes = false);

            return new HostBuilder (webHostBuilder.Build ());
        }

        public abstract class BuilderBase {
            public abstract ServiceHost Build ();
        }

        public class HostBuilder : BuilderBase {
            private readonly IWebHost _webhost;
            //etablish connection as to sending and subscribing messages to the service bus. 
            private IBusClient _bus;

            public HostBuilder (IWebHost webhost) {
                _webhost = webhost;
            }

            public BusBuilder UseRabbitMq () {
                // _webhost.Services is an out of the box IoC container
                _bus = (IBusClient) _webhost.Services.GetService (typeof (IBusClient));
                return new BusBuilder (_webhost, _bus);
            }

            public override ServiceHost Build () {
                return new ServiceHost (_webhost);
            }
        }

        public class BusBuilder : BuilderBase {
            private readonly IWebHost _webhost;
            //etablish connection as to sending and subscribing messages to the service bus. 
            private IBusClient _bus;

            public BusBuilder (IWebHost webhost, IBusClient bus) {
                _bus = bus;
                _webhost = webhost;
            }
            public BusBuilder SubscribeToCommand<TCommand> () where TCommand : ICommand {
                 var handler = (ICommandHandler<TCommand>) _webhost.Services
                    .GetService (typeof (ICommandHandler<TCommand>));

                // var serviceProvider = (IServiceProvider) _webhost.Services.GetService (typeof (IServiceProvider));
                // var handler = serviceProvider.CreateScope ().ServiceProvider.GetRequiredService<ICommandHandler<TCommand>> ();

                //this extension method should be created
                _bus.WithCommandHandlerAsync (handler);
                return this;
            }

            public BusBuilder SubscribeToEvent<TEvent> () where TEvent : IEvent {

                var serviceProvider = (IServiceProvider) _webhost.Services.GetService (typeof (IServiceProvider));
                var handler = serviceProvider.CreateScope ().ServiceProvider.GetRequiredService<IEventHandler<TEvent>> ();
                // var handler = (IEventHandler<TEvent>) _webhost.Services
                //     .GetService (typeof (IEventHandler<TEvent>));
                //this extension method should be created
                _bus.WithEventHandlerAsync (handler);
                return this;
            }

            public override ServiceHost Build () {
                return new ServiceHost (_webhost);
            }

        }

    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleAction.Common.Auth;
using SimpleAction.Common.Commands;
using SimpleAction.Common.Mongo;
using SimpleAction.Common.RabbitMq;
using SimpleAction.Services.Identity.Domain.Repositories;
using SimpleAction.Services.Identity.Domain.Services;
using SimpleAction.Services.Identity.Handlers;
using SimpleAction.Services.Identity.Repositories;
using SimpleAction.Services.Identity.Services;

namespace SimpleAction.Services.Identity {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ().AddJsonOptions (options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });;
            services.AddLogging ();
            services.AddRabbitMq (Configuration);
            services.AddMongoDB (Configuration);
            services.AddJwt (Configuration);
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler> ();
            services.AddSingleton<IEncrypter, Encrypter> ();
            services.AddScoped<IUserRepository, UserRepository> ();
            services.AddScoped<IUserService, UserService> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }
            app.ApplicationServices.GetService<IDatabaseInitializer> ().InitializeAsync ();
            app.UseMvc ();
            app.UseHttpsRedirection ();
            app.UseMvc ();

        }
    }
}
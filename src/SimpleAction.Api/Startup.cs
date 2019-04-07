// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// // using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;
// using SimpleAction.Api.Handlers;
// using SimpleAction.Api.Repositories;
// using SimpleAction.Common.Auth;
// using SimpleAction.Common.Events;
// using SimpleAction.Common.Mongo;
// using SimpleAction.Common.RabbitMq;


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
using SimpleAction.Api.Handlers;
using SimpleAction.Api.Repositories;
using SimpleAction.Common.Auth;
using SimpleAction.Common.Events;
using SimpleAction.Common.Mongo;
using SimpleAction.Common.RabbitMq;

namespace SimpleAction.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddLogging ();
            services.AddRabbitMq (Configuration);
            services.AddJwt (Configuration);
            services.AddMongoDB (Configuration);
            services.AddScoped<IEventHandler<ActivityCreated>, ActivityCreatedHandler> ();
            services.AddScoped<IEventHandler<UserAuthenticated>, UserAuthenticatedHandler> ();
            services.AddScoped<IEventHandler<UserCreated>, UserCreatedHandler> ();
            services.AddScoped<IActivityRepository, ActivityRepository> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
             if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();            
            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
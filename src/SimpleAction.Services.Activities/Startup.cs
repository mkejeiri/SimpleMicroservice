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
using SimpleAction.Common.Auth;
using SimpleAction.Common.Commands;
using SimpleAction.Common.Mongo;
using SimpleAction.Common.RabbitMq;
using SimpleAction.Services.Activities.Handlers;
using SimpleAction.Services.Activities.Repositories;
using SimpleAction.Services.Activities.Services;

namespace SimpleAction.Services.Activities {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
            services.AddLogging ();
            services.AddRabbitMq (Configuration);
            services.AddMongoDB (Configuration);
            services.AddJwt (Configuration);
            services.AddScoped<ICommandHandler<CreateActivity>, CreateActivityHandler> ();
            services.AddScoped<IActivityRepository, ActivityRepository> ();
            services.AddScoped<ICategoryRepository, CategoryRepository> ();
            services.AddScoped<IDatabaseSeeder, CustomMongoSeeder> ();
            services.AddScoped<IActivityService, ActivityService> ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            app.UseAuthentication ();
            app.ApplicationServices.GetService<IDatabaseInitializer> ().InitializeAsync ();
            app.UseHttpsRedirection ();
            app.UseMvc ();

        }
    }
}
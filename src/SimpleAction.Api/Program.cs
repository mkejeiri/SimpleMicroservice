﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleAction.Common.Events;
using SimpleAction.Common.Services;

namespace SimpleAction.Api
{
    public class Program
    {


//if 
        public static void Main(string[] args){
            ServiceHost.Create<Startup>(args)
            .UseRabbitMq()
            .SubscribeToEvent<ActivityCreated>()
            .Build()
            .Run();
        }


        //public static async Task Main(string[] args) starting C#7.0
        // public static void Main(string[] args)
        // {
        //     CreateHostBuilder(args).Build().Run();
        // }

        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             webBuilder.UseStartup<Startup>();
        //         });
    }
}
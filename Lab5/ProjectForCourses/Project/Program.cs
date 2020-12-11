using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Installers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Project.Models;

namespace Project
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
           // using(var serviceScope = host.Services.CreateScope())
            //{
                /*MainInstaller mainInstaller = new MainInstaller();
                await mainInstaller.MakeRoles(serviceScope);*/
            //}
            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

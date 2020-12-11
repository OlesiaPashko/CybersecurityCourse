using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FluentValidation;
using Project.Models.Mappings;
using Project.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
//using BLL.Services;
using Swashbuckle.AspNetCore.Swagger;
using Project.Installers;
using BLL.Installers;

namespace Project
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
            services.AddCors();
            MainInstaller mainInstaller = new MainInstaller();
            mainInstaller.InstallServices(Configuration, services);

            services.AddAuthorization();

            var installersFromPresentation = typeof(Startup).Assembly.ExportedTypes.Where(x =>
            typeof(Installers.IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance)
            .Cast<Installers.IInstaller>().ToList();

            installersFromPresentation.ForEach(installer => installer.InstallServices(Configuration, services));
            //services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
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
                app.UseHsts();
            }   
            app.UseCors(options => options.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            var swaggerOptions = new Project.Options.SwaggerOptions();
            Configuration.GetSection(nameof(Project.Options.SwaggerOptions)).Bind(swaggerOptions);

            app.UseSwagger(options =>
            {
                options.RouteTemplate = swaggerOptions.JsonRoute;
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
            });
            app.UseHttpsRedirection();
            app.UseMvc();
            //CreateRoles(serviceProvider).Wait();
        }

       /* private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = { "Admin", "User"};
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            IdentityUser user = await UserManager.FindByEmailAsync("jignesh@gmail.com");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "jignesh@gmail.com",
                    Email = "jignesh@gmail.com",
                };
                await UserManager.CreateAsync(user, "Test@123");
            }
            await UserManager.AddToRoleAsync(user, "Admin");


            IdentityUser user1 = await UserManager.FindByEmailAsync("tejas@gmail.com");

            if (user1 == null)
            {
                user1 = new IdentityUser()
                {
                    UserName = "tejas@gmail.com",
                    Email = "tejas@gmail.com",
                };
                await UserManager.CreateAsync(user1, "Test@123");
            }
            await UserManager.AddToRoleAsync(user1, "User");
        }*/

    }
}

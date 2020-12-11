using System;
using System.Collections.Generic;
using System.Text;
using BLL.Services;
using DLL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            // services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

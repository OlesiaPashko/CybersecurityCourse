﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DLL.Entities;
using DLL;
using BLL.Services;

namespace BLL.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DataContext>().AddRoles<IdentityRole>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Project.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "Project API", Version = "v1" });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[0] }
                };
                x.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization Header",
                    Name ="Authorization",
                    In = "header",
                    Type= "apiKey"
                });
                x.AddSecurityRequirement(security);
            });
        }
    }
}

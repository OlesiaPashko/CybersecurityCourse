using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DLL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Installers
{
    public class MainInstaller : IInstaller
    {
        public void InstallServices(IConfiguration configuration, IServiceCollection services)
        {
            DBInstaller dBInstaller = new DBInstaller();
            JwtInstaller jwtInstaller = new JwtInstaller();
            MapperInstaller mapperInstaller = new MapperInstaller();
            ServicesInstaller servicesInstaller = new ServicesInstaller();
            dBInstaller.InstallServices(configuration, services);
            jwtInstaller.InstallServices(configuration, services);
            mapperInstaller.InstallServices(configuration, services);
            servicesInstaller.InstallServices(configuration, services);
        }

        /*public async Task<Task> MakeRoles(IServiceScope serviceScope)
        {
            DBInstaller dBInstaller = new DBInstaller();
            return dBInstaller.MakeRoles(serviceScope);
        }*/
    }
}

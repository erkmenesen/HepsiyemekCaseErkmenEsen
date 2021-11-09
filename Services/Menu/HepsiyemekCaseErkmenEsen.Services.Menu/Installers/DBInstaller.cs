using HepsiyemekCaseErkmenEsen.Services.Menu.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiyemekCaseErkmenEsen.Services.Menu.Installers
{
    public class DBInstaller : IInstaller
    {
        public void IInstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });

            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));
        }

    }
}

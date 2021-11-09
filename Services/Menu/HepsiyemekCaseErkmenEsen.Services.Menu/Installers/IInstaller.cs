using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiyemekCaseErkmenEsen.Services.Menu.Installers
{
    public interface IInstaller
    {
        void IInstallServices(IServiceCollection services, IConfiguration configuration);
    }
}

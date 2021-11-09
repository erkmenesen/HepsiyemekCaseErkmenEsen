using HepsiyemekCaseErkmenEsen.Services.Menu.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiyemekCaseErkmenEsen.Services.Menu.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void IInstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
        }
    }
}

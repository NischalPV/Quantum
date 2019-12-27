using Microsoft.Extensions.DependencyInjection;
using Quantum.Core.Interfaces;
using Quantum.Infrastructure.Data.Repositories;
using Quantum.Infrastructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quantum.Web
{
    public class ServiceRegistry
    {
        public static void AddScopedServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}

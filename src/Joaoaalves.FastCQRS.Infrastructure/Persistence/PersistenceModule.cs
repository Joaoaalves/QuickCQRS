using Joaoaalves.FastCQRS.Application.Execution;
using Microsoft.Extensions.DependencyInjection;

namespace Joaoaalves.FastCQRS.Infrastructure.Persistence
{
    public static class PersistenceModule
    {
        public static IServiceCollection AddPersistenceModule(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
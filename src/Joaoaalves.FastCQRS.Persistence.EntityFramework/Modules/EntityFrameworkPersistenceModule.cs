using Joaoaalves.FastCQRS.Abstractions.Processing;
using Joaoaalves.FastCQRS.Persistence.Abstractions;
using Joaoaalves.FastCQRS.Persistence.EntityFramework.Adapters;
using Joaoaalves.FastCQRS.Persistence.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Joaoaalves.FastCQRS.Persistence.EntityFramework.Modules
{
    public static class EntityFrameworkPersistenceModule
    {
        public static IServiceCollection AddEfUnitOfWork<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.AddScoped<IDatabaseContext, EFDatabaseContextAdapter>();
            services.AddUnitOfWork();
            services.AddScoped<DbContext, TDbContext>();
            services.AddScoped<IDomainEventsProvider, EFDomainEventsProvider>();
            return services;
        }
    }
}

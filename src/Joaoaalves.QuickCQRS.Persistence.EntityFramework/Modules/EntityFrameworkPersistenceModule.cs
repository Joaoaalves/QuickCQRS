using Joaoaalves.QuickCQRS.Abstractions.Processing;
using Joaoaalves.QuickCQRS.Persistence.Abstractions;
using Joaoaalves.QuickCQRS.Persistence.EntityFramework.Adapters;
using Joaoaalves.QuickCQRS.Persistence.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Joaoaalves.QuickCQRS.Persistence.EntityFramework.Modules
{
    public static class EntityFrameworkPersistenceModule
    {
        public static IServiceCollection AddEfUnitOfWork<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.AddScoped<IDatabaseContext>(sp =>
            {
                var dbContext = sp.GetRequiredService<TDbContext>();
                return new EFDatabaseContextAdapter(dbContext);
            });

            services.AddUnitOfWork();
            services.AddScoped<DbContext, TDbContext>();
            services.AddScoped<IDomainEventsProvider>(sp =>
            {
                var dbContext = sp.GetRequiredService<TDbContext>();
                return new EFDomainEventsProvider(dbContext);
            });

            return services;
        }
    }
}

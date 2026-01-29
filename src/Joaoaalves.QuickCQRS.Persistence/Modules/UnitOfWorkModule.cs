using Joaoaalves.QuickCQRS.Abstractions.Processing;
using Joaoaalves.QuickCQRS.Abstractions.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Joaoaalves.QuickCQRS.Persistence.Modules
{
    public static class UnitOfWorkModule
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRequestPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
            return services;
        }
    }
}
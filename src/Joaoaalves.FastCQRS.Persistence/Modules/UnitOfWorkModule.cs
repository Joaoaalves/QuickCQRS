using Joaoaalves.FastCQRS.Abstractions.Processing;
using Joaoaalves.FastCQRS.Abstractions.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Joaoaalves.FastCQRS.Persistence.Modules
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
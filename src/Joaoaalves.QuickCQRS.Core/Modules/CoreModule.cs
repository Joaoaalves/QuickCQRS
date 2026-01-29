using System.Reflection;
using Joaoaalves.QuickCQRS.Abstractions.Processing;
using Microsoft.Extensions.DependencyInjection;
using Joaoaalves.QuickCQRS.Core.Processing;
using Joaoaalves.QuickCQRS.Core.Events;
using Joaoaalves.QuickCQRS.Abstractions.Notifications;
using Joaoaalves.QuickCQRS.Abstractions.Requests;
using Joaoaalves.QuickCQRS.Core.Validation;

namespace Joaoaalves.QuickCQRS.Core.Modules
{
    /// <summary>
    /// Provides methods to configure the mediator module, including command/query handlers and pipeline behaviors.
    /// </summary>
    public static class CoreModule
    {
        /// <summary>
        /// Registers MediatR-like services and command/query processing pipeline.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="args">Assemblies or assembly name prefixes to scan for handlers.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddFastCQRS(this IServiceCollection services, Action<FastCQRSOptions>? configure = null)
        {
            var options = new FastCQRSOptions();
            configure?.Invoke(options);

            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            var assemblies = ResolveAssemblies(options);
            RegisterHandlers(services, assemblies, typeof(INotificationHandler<>));
            RegisterHandlers(services, assemblies, typeof(IRequestHandler<,>));

            services.AddScoped<CommandsExecutor>();
            services.AddScoped<QueriesExecutor>();

            services.AddScoped(typeof(IRequestPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));

            return services;
        }

        /// <summary>
        /// Resolves assemblies from input parameters to be scanned for handler types.
        /// </summary>
        /// <param name="args">Can be an array of assemblies or string prefixes.</param>
        /// <returns>An array of resolved assemblies.</returns>
        public static Assembly[] ResolveAssemblies(FastCQRSOptions options)
        {
            if (options.Assemblies.Length != 0)
            {
                // Force load assemblies
                foreach (var assembly in options.Assemblies)
                {
                    _ = assembly.GetTypes();
                }

                return [.. options.Assemblies.Distinct()];
            }

            // Fallback to all loaded assemblies
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.FullName))
                .ToArray();
        }

        /// <summary>
        /// Registers all types implementing a specific handler interface (e.g., IRequestHandler, INotificationHandler).
        /// </summary>
        public static void RegisterHandlers(IServiceCollection services, Assembly[] assemblies, Type handlerInterface)
        {
            var types = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToList();

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface);

                foreach (var @interface in interfaces)
                {
                    services.AddTransient(@interface, type);
                }
            }
        }
    }
}
using System.Reflection;
using Joaoaalves.FastCQRS.Abstractions.Processing;
using Microsoft.Extensions.DependencyInjection;
using Joaoaalves.FastCQRS.Core.Processing;
using Joaoaalves.FastCQRS.Core.Events;
using Joaoaalves.FastCQRS.Abstractions.Notifications;
using Joaoaalves.FastCQRS.Abstractions.Requests;
using Joaoaalves.FastCQRS.Core.Validation;

namespace Joaoaalves.FastCQRS.Core.Modules
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
        public static IServiceCollection AddFastCQRS(this IServiceCollection services, params object[] args)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            var assemblies = ResolveAssemblies(args);
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
        public static Assembly[] ResolveAssemblies(object[] args)
        {
            if (args == null || args.Length == 0)
                return AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.FullName))
                    .ToArray();

            if (args.All(a => a is Assembly))
                return args.Cast<Assembly>().ToArray();

            if (args.All(a => a is string))
            {
                var prefixes = args.Cast<string>().ToArray();

                return AppDomain.CurrentDomain
                    .GetAssemblies()
                    .Where(a =>
                        !a.IsDynamic &&
                        !string.IsNullOrWhiteSpace(a.FullName) &&
                        prefixes.Any(p => a.FullName!.StartsWith(p))
                    ).ToArray();
            }

            throw new ArgumentException("Invalid Parameters for AddMediatorModule.");
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

                foreach (var intrfc in interfaces)
                {
                    services.AddTransient(intrfc, type);
                }
            }
        }
    }
}
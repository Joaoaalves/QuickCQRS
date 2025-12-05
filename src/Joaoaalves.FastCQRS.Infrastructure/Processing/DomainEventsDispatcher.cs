using Joaoaalves.DDD.Common;
using Joaoaalves.DDD.Events;
using Joaoaalves.FastCQRS.Application.DomainEvents;
using Joaoaalves.FastCQRS.Application.Execution;
using Joaoaalves.FastCQRS.Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Joaoaalves.FastCQRS.Infrastructure.Processing
{
    /// <summary>
    /// Dispatches domain events collected from tracked entities.
    /// </summary>
    /// <param name="mediator">The mediator used to publish domain events.</param>
    /// <param name="context">The database context from which domain events are collected.</param>
    public class DomainEventsDispatcher(IMediator mediator, DbContext context) : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        private readonly DbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <inheritdoc />
        public async Task DispatchEventsAsync()
        {
            var domainEntities = _context.ChangeTracker
                .Entries<Entity>()
                .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Count != 0)
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(e => e.Entity.DomainEvents!)
                .ToList();

            domainEntities.ForEach(e => e.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                var notification = CreateNotification(domainEvent);

                await _mediator.Publish(notification);
            }
        }

        private static INotification CreateNotification(IDomainEvent domainEvent)
        {
            var wrapperType = typeof(DomainNotificationBase<>)
                .MakeGenericType(domainEvent.GetType());

            return (INotification)Activator.CreateInstance(wrapperType, domainEvent)!;
        }
    }
}
using Joaoaalves.DDD.Events;
using Joaoaalves.QuickCQRS.Abstractions.Notifications;
using Joaoaalves.QuickCQRS.Abstractions.Processing;

namespace Joaoaalves.QuickCQRS.Core.Events
{
    public class DomainEventsDispatcher(IMediator mediator, IDomainEventsProvider provider)
        : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator = mediator
            ?? throw new ArgumentNullException(
                "Mediator was not provided for DomainEventsDispatcher"
            );

        private readonly IDomainEventsProvider _provider = provider
            ?? throw new ArgumentNullException(
                "DomainEventsProvider was not provided for DomainEventsDispatcher"
            );

        public async Task DispatchEventsAsync()
        {
            var events = _provider.CollectDomainEvents();
            _provider.ClearCollectedDomainEvents();

            foreach (var domainEvent in events)
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

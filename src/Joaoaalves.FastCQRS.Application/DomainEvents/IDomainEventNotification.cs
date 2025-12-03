using Joaoaalves.FastCQRS.Domain.Notifications;

namespace Joaoaalves.FastCQRS.Application.DomainEvents
{
    public interface IDomainEventNotification<TEventType> : IDomainEventNotification
    {
        TEventType DomainEvent { get; }
    }

    public interface IDomainEventNotification : INotification
    {
        Guid Id { get; }
    }
}
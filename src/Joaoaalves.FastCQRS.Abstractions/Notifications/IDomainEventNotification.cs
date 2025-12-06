namespace Joaoaalves.FastCQRS.Abstractions.Notifications
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
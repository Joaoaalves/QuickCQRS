using System.Text.Json.Serialization;
using Joaoaalves.DDD.Events;

namespace Joaoaalves.FastCQRS.Application.DomainEvents
{
    public class DomainNotificationBase<T>(T domainEvent) : IDomainEventNotification<T> where T : IDomainEvent
    {
        [JsonIgnore]
        public T DomainEvent { get; } = domainEvent;

        public Guid Id { get; } = Guid.NewGuid();
    }
}
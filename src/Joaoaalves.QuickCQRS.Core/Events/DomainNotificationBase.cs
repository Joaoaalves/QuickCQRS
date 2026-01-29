using System.Text.Json.Serialization;
using Joaoaalves.DDD.Events;
using Joaoaalves.QuickCQRS.Abstractions.Notifications;

namespace Joaoaalves.QuickCQRS.Core.Events
{
    public class DomainNotificationBase<T>(T domainEvent) : IDomainEventNotification<T> where T : IDomainEvent
    {
        [JsonIgnore]
        public T DomainEvent { get; } = domainEvent;

        public Guid Id { get; } = Guid.NewGuid();
    }
}
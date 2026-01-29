using Joaoaalves.DDD.Events;

namespace Joaoaalves.QuickCQRS.Core.Tests.Fakes
{
    public class FakeDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn => new();
    }
}
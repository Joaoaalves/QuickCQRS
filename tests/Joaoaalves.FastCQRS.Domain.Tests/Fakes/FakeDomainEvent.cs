using Joaoaalves.DDD.Events;

namespace Joaoaalves.FastCQRS.Domain.Tests.Fakes
{
    public class FakeDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn => new();
    }
}
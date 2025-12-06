using Joaoaalves.DDD.Events;

namespace Joaoaalves.FastCQRS.Core.Tests.Fakes
{
    public class FakeDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn => new();
    }
}
using Joaoaalves.FastCQRS.Domain.DDD;

namespace Joaoaalves.FastCQRS.Domain.Tests.Fakes
{
    public class FakeDomainEvent : IDomainEvent
    {
        public DateTime OccurredOn => new();
    }
}
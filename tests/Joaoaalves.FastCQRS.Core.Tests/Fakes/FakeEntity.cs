using Joaoaalves.DDD.Common;
using Joaoaalves.DDD.Events;
using Joaoaalves.DDD.Rules;

namespace Joaoaalves.FastCQRS.Core.Tests.Fakes
{
    public class FakeEntity : Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public FakeEntity()
        {
            Name = "";
        }

        public FakeEntity(string name)
        {
            Name = name;
        }

        public void AddEvent(IDomainEvent domainEvent) => AddDomainEvent(domainEvent);
        public void ValidateRule(IBusinessRule rule) => CheckRule(rule);
    }
}
using Joaoaalves.DDD.Events;

namespace Joaoaalves.FastCQRS.Abstractions.Processing
{
    public interface IDomainEventsProvider
    {
        IReadOnlyCollection<IDomainEvent> CollectDomainEvents();
        void ClearCollectedDomainEvents();
    }
}

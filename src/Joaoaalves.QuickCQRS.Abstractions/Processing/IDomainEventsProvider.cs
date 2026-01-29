using Joaoaalves.DDD.Events;

namespace Joaoaalves.QuickCQRS.Abstractions.Processing
{
    public interface IDomainEventsProvider
    {
        IReadOnlyCollection<IDomainEvent> CollectDomainEvents();
        void ClearCollectedDomainEvents();
    }
}

using Joaoaalves.DDD.Common;
using Joaoaalves.DDD.Events;
using Joaoaalves.FastCQRS.Abstractions.Processing;
using Microsoft.EntityFrameworkCore;

namespace Joaoaalves.FastCQRS.Persistence.EntityFramework.Adapters
{
    public class EFDomainEventsProvider(DbContext db) : IDomainEventsProvider
    {
        private readonly DbContext _db = db;

        public IReadOnlyCollection<IDomainEvent> CollectDomainEvents()
        {
            return _db.ChangeTracker.Entries<Entity>()
                .Where(e => e.Entity.DomainEvents is { Count: > 0 })
                .SelectMany(e => e.Entity.DomainEvents!)
                .ToList();
        }

        public void ClearCollectedDomainEvents()
        {
            var entities = _db.ChangeTracker.Entries<Entity>()
                .Where(e => e.Entity.DomainEvents is { Count: > 0 });

            foreach (var entry in entities)
                entry.Entity.ClearDomainEvents();
        }
    }
}

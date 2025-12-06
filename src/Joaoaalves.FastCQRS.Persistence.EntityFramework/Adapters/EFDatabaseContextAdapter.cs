using Microsoft.EntityFrameworkCore;
using Joaoaalves.DDD.Common;
using Joaoaalves.FastCQRS.Persistence.Abstractions;

namespace Joaoaalves.FastCQRS.Persistence.EntityFramework.Adapters
{
    public class EFDatabaseContextAdapter(DbContext context) : IDatabaseContext
    {
        private readonly DbContext _context = context;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void RevertChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;

                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}

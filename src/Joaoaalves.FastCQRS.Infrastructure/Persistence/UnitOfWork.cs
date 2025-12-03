using Joaoaalves.FastCQRS.Application.Execution;
using Joaoaalves.FastCQRS.Domain.DDD;
using Joaoaalves.FastCQRS.Infrastructure.Processing;
using Microsoft.EntityFrameworkCore;

namespace Joaoaalves.FastCQRS.Infrastructure.Persistence
{
    public class UnitOfWork(
        DbContext context,
        IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork
    {
        private readonly DbContext _context = context;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher = domainEventsDispatcher;
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public Task RevertAsync()
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
            return Task.CompletedTask;
        }
    }
}
using Joaoaalves.QuickCQRS.Abstractions.Processing;
using Joaoaalves.QuickCQRS.Persistence.Abstractions;

namespace Joaoaalves.QuickCQRS.Persistence
{
    public class UnitOfWork(
        IDatabaseContext context,
        IDomainEventsDispatcher domainEventsDispatcher) : IUnitOfWork
    {
        private readonly IDatabaseContext _context = context;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher = domainEventsDispatcher;
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public Task RevertAsync()
        {
            _context.RevertChanges();
            return Task.CompletedTask;
        }
    }
}
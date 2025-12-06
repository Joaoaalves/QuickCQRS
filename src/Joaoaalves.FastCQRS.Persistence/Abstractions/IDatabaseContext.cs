namespace Joaoaalves.FastCQRS.Persistence.Abstractions
{
    public interface IDatabaseContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Called when the UnitOfWork must revert all pending changes.
        /// Implementations must ensure a full rollback of tracked changes.
        /// </summary>
        void RevertChanges();
    }
}

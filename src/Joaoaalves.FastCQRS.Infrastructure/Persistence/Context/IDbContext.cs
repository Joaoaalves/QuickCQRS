namespace Joaoaalves.FastCQRS.Infrastructure.Persistence.Context
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IChangeTracker ChangeTracker { get; }
    }
}
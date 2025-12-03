using Joaoaalves.FastCQRS.Domain.DDD;

namespace Joaoaalves.FastCQRS.Infrastructure.Persistence.Context
{
    public interface IChangeTracker
    {
        IEnumerable<IEntry<TEntity>> Entries<TEntity>() where TEntity : Entity;
    }
}
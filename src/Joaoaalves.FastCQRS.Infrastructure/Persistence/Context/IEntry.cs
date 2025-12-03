using Joaoaalves.FastCQRS.Domain.DDD;

namespace Joaoaalves.FastCQRS.Infrastructure.Persistence.Context
{
    public interface IEntry<TEntity> where TEntity : Entity
    {
        TEntity Entity { get; }
        EntityState State { get; set; }
    }
}
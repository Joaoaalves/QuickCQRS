namespace Joaoaalves.FastCQRS.Infrastructure.Persistence.Context
{
    public enum EntityState
    {
        Unchanged,
        Modified,
        Added,
        Deleted,
        Detached
    }
}
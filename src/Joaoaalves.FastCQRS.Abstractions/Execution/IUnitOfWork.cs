namespace Joaoaalves.FastCQRS.Abstractions.Execution
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task RevertAsync();
    }
}

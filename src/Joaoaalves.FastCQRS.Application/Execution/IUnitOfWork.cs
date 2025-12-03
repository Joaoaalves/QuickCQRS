namespace Joaoaalves.FastCQRS.Application.Execution
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task RevertAsync();
    }
}

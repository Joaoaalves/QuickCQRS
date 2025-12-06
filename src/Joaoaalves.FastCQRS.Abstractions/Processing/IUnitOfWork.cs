namespace Joaoaalves.FastCQRS.Abstractions.Processing
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task RevertAsync();
    }
}

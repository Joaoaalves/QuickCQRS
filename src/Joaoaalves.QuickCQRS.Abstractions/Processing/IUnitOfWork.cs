namespace Joaoaalves.QuickCQRS.Abstractions.Processing
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task RevertAsync();
    }
}

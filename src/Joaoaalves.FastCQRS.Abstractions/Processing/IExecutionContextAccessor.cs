namespace Joaoaalves.FastCQRS.Abstractions.Processing
{
    public interface IExecutionContextAccessor
    {
        Guid CorrelationId { get; }
        bool IsAvailable { get; }
    }
}
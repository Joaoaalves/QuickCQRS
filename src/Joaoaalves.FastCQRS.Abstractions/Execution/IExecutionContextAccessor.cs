namespace Joaoaalves.FastCQRS.Abstractions.Execution
{
    public interface IExecutionContextAccessor
    {
        Guid CorrelationId { get; }
        bool IsAvailable { get; }
    }
}
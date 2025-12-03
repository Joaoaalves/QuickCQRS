namespace Joaoaalves.FastCQRS.Application.Execution
{
    public interface IExecutionContextAccessor
    {
        Guid CorrelationId { get; }
        bool IsAvailable { get; }
    }
}
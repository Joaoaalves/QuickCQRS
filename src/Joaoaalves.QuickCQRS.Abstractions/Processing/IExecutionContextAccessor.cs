namespace Joaoaalves.QuickCQRS.Abstractions.Processing
{
    public interface IExecutionContextAccessor
    {
        Guid CorrelationId { get; }
        bool IsAvailable { get; }
    }
}
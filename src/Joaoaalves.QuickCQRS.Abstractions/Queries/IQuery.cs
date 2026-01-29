using Joaoaalves.QuickCQRS.Abstractions.Requests;

namespace Joaoaalves.QuickCQRS.Abstractions.Queries
{
    public interface IQuery<TResult> : IRequest<TResult> { }
}
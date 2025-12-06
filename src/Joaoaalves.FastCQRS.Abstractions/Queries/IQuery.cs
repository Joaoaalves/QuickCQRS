using Joaoaalves.FastCQRS.Abstractions.Requests;

namespace Joaoaalves.FastCQRS.Abstractions.Queries
{
    public interface IQuery<TResult> : IRequest<TResult> { }
}
using Joaoaalves.FastCQRS.Domain.Requests;

namespace Joaoaalves.FastCQRS.Application.Queries
{
    public interface IQuery<TResult> : IRequest<TResult> { }
}
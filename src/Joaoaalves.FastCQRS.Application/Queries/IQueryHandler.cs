using Joaoaalves.FastCQRS.Domain.Requests;

namespace Joaoaalves.FastCQRS.Application.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}
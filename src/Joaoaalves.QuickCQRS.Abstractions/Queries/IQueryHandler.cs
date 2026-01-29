using Joaoaalves.QuickCQRS.Abstractions.Requests;

namespace Joaoaalves.QuickCQRS.Abstractions.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {

    }
}
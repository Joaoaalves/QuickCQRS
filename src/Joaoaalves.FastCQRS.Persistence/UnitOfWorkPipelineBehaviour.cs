using Joaoaalves.FastCQRS.Abstractions.Commands;
using Joaoaalves.FastCQRS.Abstractions.Processing;
using Joaoaalves.FastCQRS.Abstractions.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Joaoaalves.FastCQRS.Persistence
{
    /// <summary>
    /// Pipeline behavior that ensures Unit of Work is committed if the command succeeds, or reverted if an exception occurs.
    /// </summary>
    /// <typeparam name="TRequest">The command type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public class UnitOfWorkPipelineBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IRequestPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IUnitOfWork _uow = unitOfWork;
        /// <inheritdoc />
        public async Task<TResponse> Handle(TRequest request, Func<Task<TResponse>> next, CancellationToken cancellationToken)
        {
            try
            {
                var result = await next();
                var res = await _uow.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await _uow.RevertAsync();
                throw;
            }
        }
    }
}
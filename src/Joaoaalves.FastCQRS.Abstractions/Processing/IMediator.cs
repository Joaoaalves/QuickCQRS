using Joaoaalves.FastCQRS.Abstractions.Notifications;
using Joaoaalves.FastCQRS.Abstractions.Requests;

namespace Joaoaalves.FastCQRS.Abstractions.Processing
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;
    }
}
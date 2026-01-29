using Joaoaalves.QuickCQRS.Abstractions.Notifications;
using Joaoaalves.QuickCQRS.Abstractions.Requests;

namespace Joaoaalves.QuickCQRS.Abstractions.Processing
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
        Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;
    }
}
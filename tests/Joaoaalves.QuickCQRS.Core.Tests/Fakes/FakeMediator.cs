
using Joaoaalves.QuickCQRS.Abstractions.Notifications;
using Joaoaalves.QuickCQRS.Abstractions.Processing;
using Joaoaalves.QuickCQRS.Abstractions.Requests;

namespace Joaoaalves.QuickCQRS.Core.Tests.Fakes
{
    public class FakeMediator : IMediator
    {
        public readonly List<INotification> PublishedEvents = new();

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Not needed for these tests");
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            PublishedEvents.Add(notification);
            return Task.CompletedTask;
        }
    }
}
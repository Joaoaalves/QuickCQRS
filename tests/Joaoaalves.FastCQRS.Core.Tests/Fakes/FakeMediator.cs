
using Joaoaalves.FastCQRS.Abstractions.Notifications;
using Joaoaalves.FastCQRS.Abstractions.Processing;
using Joaoaalves.FastCQRS.Abstractions.Requests;

namespace Joaoaalves.FastCQRS.Core.Tests.Fakes
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
using Joaoaalves.FastCQRS.Application.Execution;
using Joaoaalves.FastCQRS.Domain.Notifications;
using Joaoaalves.FastCQRS.Domain.Requests;

namespace Joaoaalves.FastCQRS.Infrastructure.Tests.Fakes
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
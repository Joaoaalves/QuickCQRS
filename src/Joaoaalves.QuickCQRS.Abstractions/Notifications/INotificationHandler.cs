namespace Joaoaalves.QuickCQRS.Abstractions.Notifications
{
    public interface INotificationHandler<TNotification> where TNotification : INotification
    {
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
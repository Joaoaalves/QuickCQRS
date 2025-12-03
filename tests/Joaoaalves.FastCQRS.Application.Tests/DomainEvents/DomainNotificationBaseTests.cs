
using Joaoaalves.FastCQRS.Application.DomainEvents;
using Joaoaalves.FastCQRS.Domain.Tests.Fakes;

namespace Joaoaalves.FastCQRS.Application.Tests.DomainEvents
{
    public class DomainNotificationBaseTests
    {
        [Fact]
        public void Constructor_ShouldSetDomainEvent()
        {
            // Arrange
            var domainEvent = new FakeDomainEvent();

            // Act
            var notification = new DomainNotificationBase<FakeDomainEvent>(domainEvent);

            // Assert
            Assert.Equal(domainEvent, notification.DomainEvent);
        }

        [Fact]
        public void Constructor_ShouldGenerateId()
        {
            // Arrange
            var domainEvent = new FakeDomainEvent();

            // Act
            var notification = new DomainNotificationBase<FakeDomainEvent>(domainEvent);

            // Assert
            Assert.NotEqual(Guid.Empty, notification.Id);
        }
    }
}
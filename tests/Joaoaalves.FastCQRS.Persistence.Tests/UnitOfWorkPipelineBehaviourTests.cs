using Joaoaalves.FastCQRS.Abstractions.Commands;
using Joaoaalves.FastCQRS.Abstractions.Processing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Joaoaalves.FastCQRS.Persistence.Tests
{
    public class UnitOfWorkPipelineBehaviorTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly ServiceProvider _serviceProvider;

        public UnitOfWorkPipelineBehaviorTests()
        {
            _uowMock = new Mock<IUnitOfWork>();

            var services = new ServiceCollection();
            services.AddSingleton(_uowMock.Object);
            _serviceProvider = services.BuildServiceProvider();
        }

        private record TestCommand(Guid Id) : ICommand<Unit>;

        [Fact]
        public async Task Handle_WhenCommandSucceeds_ShouldCommit()
        {
            // Arrange
            var behavior = new UnitOfWorkPipelineBehavior<TestCommand, Unit>(_serviceProvider);
            var command = new TestCommand(Guid.NewGuid());

            Task<Unit> Next() => Task.FromResult(Unit.Value);

            // Act
            var result = await behavior.Handle(command, Next, CancellationToken.None);

            // Assert
            Assert.Equal(Unit.Value, result);

            _uowMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
            _uowMock.Verify(u => u.RevertAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_WhenCommandThrows_ShouldRevert()
        {
            // Arrange
            var behavior = new UnitOfWorkPipelineBehavior<TestCommand, Unit>(_serviceProvider);
            var command = new TestCommand(Guid.NewGuid());

            Task<Unit> Next() => throw new InvalidOperationException("Test exception");

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                behavior.Handle(command, Next, CancellationToken.None));

            Assert.Equal("Test exception", ex.Message);

            _uowMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
            _uowMock.Verify(u => u.RevertAsync(), Times.Once);
        }
    }
}

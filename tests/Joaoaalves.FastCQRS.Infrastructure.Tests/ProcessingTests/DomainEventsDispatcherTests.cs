using Joaoaalves.FastCQRS.Domain.Tests.Fakes;
using Joaoaalves.FastCQRS.Infrastructure.Processing;
using Joaoaalves.FastCQRS.Infrastructure.Tests.Builders;
using Joaoaalves.FastCQRS.Infrastructure.Tests.Fakes;

namespace Joaoaalves.FastCQRS.Infrastructure.Tests.ProcessingTests
{
    public class DomainEventsDispatcherTests
    {
        private readonly FakeDbContext<FakeEntity> _context;

        public DomainEventsDispatcherTests()
        {
            _context = DatabaseBuilder<FakeEntity>.InMemoryDatabase();
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenMediatorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DomainEventsDispatcher(null!, _context));
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenContextIsNull()
        {
            var mediator = new FakeMediator();

            Assert.Throws<ArgumentNullException>(() => new DomainEventsDispatcher(mediator, null!));
        }

        [Fact]
        public async Task DispatchEventsAsync_ShouldDoNothing_WhenNoEntities()
        {
            var mediator = new FakeMediator();
            var dispatcher = new DomainEventsDispatcher(mediator, _context);

            await dispatcher.DispatchEventsAsync();

            Assert.Empty(mediator.PublishedEvents);
        }

        [Fact]
        public async Task DispatchEventsAsync_ShouldIgnoreEntitiesWithoutEvents()
        {
            var mediator = new FakeMediator();
            var dispatcher = new DomainEventsDispatcher(mediator, _context);

            _context.Entities.Add(new FakeEntity());
            await _context.SaveChangesAsync();

            await dispatcher.DispatchEventsAsync();

            Assert.Empty(mediator.PublishedEvents);
        }

        [Fact]
        public async Task DispatchEventsAsync_ShouldPublishDomainEvents_AndClearThem()
        {
            var mediator = new FakeMediator();
            var dispatcher = new DomainEventsDispatcher(mediator, _context);

            var entity = new FakeEntity();
            entity.AddEvent(new FakeDomainEvent());
            entity.AddEvent(new FakeDomainEvent());

            _context.Entities.Add(entity);
            await _context.SaveChangesAsync();

            await dispatcher.DispatchEventsAsync();

            Assert.Equal(2, mediator.PublishedEvents.Count);
            Assert.Empty(entity.DomainEvents ?? []);
        }
    }
}
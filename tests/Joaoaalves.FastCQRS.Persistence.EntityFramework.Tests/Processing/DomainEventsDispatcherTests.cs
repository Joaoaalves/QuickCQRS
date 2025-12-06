
using Joaoaalves.FastCQRS.Abstractions.Processing;
using Joaoaalves.FastCQRS.Core.Events;
using Joaoaalves.FastCQRS.Core.Tests.Fakes;
using Joaoaalves.FastCQRS.Persistence.EntityFramework.Adapters;
using Joaoaalves.FastCQRS.Persistence.EntityFramework.Tests.Builders;
using Joaoaalves.FastCQRS.Persistence.EntityFramework.Tests.Fakes;

namespace Joaoaalves.FastCQRS.Persistence.EntityFramework.Tests.Processing
{
    public class DomainEventsDispatcherTests
    {
        private readonly FakeDbContext<FakeEntity> _context;
        private readonly IDomainEventsProvider provider;
        public DomainEventsDispatcherTests()
        {
            _context = DatabaseBuilder<FakeEntity>.InMemoryDatabase();
            provider = new EFDomainEventsProvider(_context);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenMediatorIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DomainEventsDispatcher(null!, provider));
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
            var dispatcher = new DomainEventsDispatcher(mediator, provider);

            await dispatcher.DispatchEventsAsync();

            Assert.Empty(mediator.PublishedEvents);
        }

        [Fact]
        public async Task DispatchEventsAsync_ShouldIgnoreEntitiesWithoutEvents()
        {
            var mediator = new FakeMediator();
            var dispatcher = new DomainEventsDispatcher(mediator, provider);

            _context.Entities.Add(new FakeEntity());
            await _context.SaveChangesAsync();

            await dispatcher.DispatchEventsAsync();

            Assert.Empty(mediator.PublishedEvents);
        }

        [Fact]
        public async Task DispatchEventsAsync_ShouldPublishDomainEvents_AndClearThem()
        {
            var mediator = new FakeMediator();
            var dispatcher = new DomainEventsDispatcher(mediator, provider);

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
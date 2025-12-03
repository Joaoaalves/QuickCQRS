using Joaoaalves.FastCQRS.Infrastructure.Tests.Fakes;
using Microsoft.EntityFrameworkCore;

namespace Joaoaalves.FastCQRS.Infrastructure.Tests.Builders
{
    public static class DatabaseBuilder<T> where T : class
    {
        private static readonly DbContextOptions<FakeDbContext<T>> _options =
            new DbContextOptionsBuilder<FakeDbContext<T>>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        public static FakeDbContext<T> InMemoryDatabase()
        {
            return new FakeDbContext<T>(_options);
        }
    }
}
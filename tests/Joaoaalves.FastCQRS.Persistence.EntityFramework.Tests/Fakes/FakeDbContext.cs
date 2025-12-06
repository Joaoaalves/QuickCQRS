using Microsoft.EntityFrameworkCore;

namespace Joaoaalves.FastCQRS.Persistence.EntityFramework.Tests.Fakes
{
    public class FakeDbContext<T>(DbContextOptions<FakeDbContext<T>> options) : DbContext(options) where T : class
    {

        public DbSet<T> Entities => Set<T>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }

}
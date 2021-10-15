using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class FooContext : DbContext, IFooContext
    {
        public DbSet<EntityT> Entities { get; set; }

        public FooContext(DbContextOptions<FooContext> options) : base(options)
        {
            // db password = b3b28432-360f-4676-9006-b6a7f7f1ea47
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // ...
        }
    }
}
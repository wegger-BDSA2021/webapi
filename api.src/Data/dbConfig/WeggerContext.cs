using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class WeggerContext : DbContext, IWeggerContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public WeggerContext(DbContextOptions<WeggerContext> options) : base(options)
        {
            // db password = b3b28432-360f-4676-9006-b6a7f7f1ea47
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // ...
        }

    }
}
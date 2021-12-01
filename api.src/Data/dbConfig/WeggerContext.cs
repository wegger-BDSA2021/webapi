using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class WeggerContext : DbContext, IWeggerContext
    {
        // public DbSet<User> Users { get; set; }
        // public DbSet<Rating> Ratings { get; set; }
        // public DbSet<Resource> Resources { get; set; }
        // public DbSet<Tag> Tags { get; set; }
        // public DbSet<Comment> Comments { get; set; }

        public DbSet<User> Users => Set<User>();
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Comment> Comments => Set<Comment>();


        public WeggerContext(DbContextOptions<WeggerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // ...
        }

    }
}
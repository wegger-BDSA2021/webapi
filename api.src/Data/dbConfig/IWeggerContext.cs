
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public interface IWeggerContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
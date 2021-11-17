
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public interface IWeggerContext 
    {
        DbSet<User> Users { get; set; }
        DbSet<Rating> Ratings { get; set; }
        DbSet<Resource> Resources { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Comment> Comments { get; set; }

        int SaveChanges();
    }
}
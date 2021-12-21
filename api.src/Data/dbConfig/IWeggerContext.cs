using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public interface IWeggerContext : IDisposable
    {
        DbSet<User> Users { get; }
        DbSet<Rating> Ratings { get; }
        DbSet<Resource> Resources { get; }
        DbSet<Tag> Tags { get; }
        DbSet<Comment> Comments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        int SaveChanges();
    }
}
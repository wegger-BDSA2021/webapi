using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Data
{
    public interface IWeggerContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Rating> Ratings { get; set; }
        DbSet<Resource> Resources { get; set; }
        DbSet<Tag> Tags { get; set; }
        DbSet<Comment> Comments { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        int SaveChanges();
    }
}
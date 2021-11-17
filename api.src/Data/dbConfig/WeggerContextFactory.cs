using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Data
{
    public class WeggerContextFactory : IDesignTimeDbContextFactory<WeggerContext>
    {
        public WeggerContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<WeggerContextFactory>()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Wegger");

            var optionsBuilder = new DbContextOptionsBuilder<WeggerContext>()
                .UseSqlServer(connectionString);

            return new WeggerContext(optionsBuilder.Options);
        }
    }
}
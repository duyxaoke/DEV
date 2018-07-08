using System.IO;
using Equinox.Domain.Core.Events;
using Equinox.Domain.Models;
using Equinox.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Equinox.Infra.Data.Context
{
    public class EquinoxContext : DbContext
    {
        public DbSet<StoredEvent> StoredEvent { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Refferal> Refferals { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new ConfigMap());
            modelBuilder.ApplyConfiguration(new RefferalMap());
            modelBuilder.ApplyConfiguration(new TransactionMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}